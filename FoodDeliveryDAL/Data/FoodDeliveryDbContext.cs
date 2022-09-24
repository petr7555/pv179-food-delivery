using System.Configuration;
using FoodDeliveryDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryDAL.Data;

public class FoodDeliveryDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var environment = ConfigurationManager.AppSettings["Environment"];
        var connectionString = environment switch
        {
            null => throw new Exception("Missing environment."),
            "Development" => ConfigurationManager.AppSettings["ConnectionStringDevelopment"],
            "Production" => ConfigurationManager.AppSettings["ConnectionStringProduction"],
            _ => throw new Exception($"Invalid environment {environment}")
        };
        if (connectionString is null)
        {
            throw new Exception("Missing connection string.");
        }

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}

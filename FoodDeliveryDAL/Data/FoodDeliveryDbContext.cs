using FoodDeliveryDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryDAL.Data;

public class FoodDeliveryDbContext : DbContext
{
    private const string DatabaseName = "PizzaShop";
    private const string ConnectionString = $"Host=localhost;Database={DatabaseName}";

    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}

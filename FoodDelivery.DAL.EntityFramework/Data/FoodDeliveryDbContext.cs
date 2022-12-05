using FoodDelivery.DAL.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Data;

public class FoodDeliveryDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Category> Categories { get; set; }

    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        modelBuilder.Entity<Product>()
            .Navigation(p => p.Price)
            .AutoInclude();

        modelBuilder.Entity<Price>()
            .Navigation(p => p.Currency)
            .AutoInclude();

        modelBuilder.Entity<Category>()
            .Navigation(c => c.Products)
            .AutoInclude();

        modelBuilder.Entity<Product>()
            .Navigation(p => p.Restaurant)
            .AutoInclude();

        modelBuilder.Entity<Order>()
            .Navigation(o => o.OrderProducts)
            .AutoInclude();

        base.OnModelCreating(modelBuilder);
    }
}

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
            .Navigation(p => p.Prices)
            .AutoInclude();

        modelBuilder.Entity<Price>()
            .Navigation(p => p.Currency)
            .AutoInclude();

        modelBuilder.Entity<Product>()
            .Navigation(p => p.Restaurant)
            .AutoInclude();

        modelBuilder.Entity<OrderProduct>()
            .Navigation(op => op.Product)
            .AutoInclude();

        modelBuilder.Entity<Restaurant>()
            .Navigation(r => r.DeliveryPrices)
            .AutoInclude();
        
        modelBuilder.Entity<Restaurant>()
            .Navigation(r => r.Ratings)
            .AutoInclude();

        modelBuilder.Entity<Category>()
            .Navigation(c => c.Products)
            .AutoInclude();

        modelBuilder.Entity<Order>()
            .Navigation(o => o.CustomerDetails)
            .AutoInclude();

        modelBuilder.Entity<Order>()
            .Navigation(o => o.Coupons)
            .AutoInclude();
        
        modelBuilder.Entity<Order>()
            .Navigation(o => o.OrderProducts)
            .AutoInclude();
                
        modelBuilder.Entity<Order>()
            .Navigation(o => o.Rating)
            .AutoInclude();
        
        modelBuilder.Entity<Coupon>()
            .Navigation(c => c.Prices)
            .AutoInclude();

        modelBuilder.Entity<CustomerDetails>()
            .Navigation(cd => cd.Customer)
            .AutoInclude();

        modelBuilder.Entity<CustomerDetails>()
            .Navigation(cd => cd.BillingAddress)
            .AutoInclude();

        modelBuilder.Entity<CustomerDetails>()
            .Navigation(cd => cd.DeliveryAddress)
            .AutoInclude();

        modelBuilder.Entity<CustomerDetails>()
            .Navigation(cd => cd.CompanyInfo)
            .AutoInclude();

        modelBuilder.Entity<User>()
            .Navigation(u => u.CustomerDetails)
            .AutoInclude();

        modelBuilder.Entity<User>()
            .Navigation(u => u.UserSettings)
            .AutoInclude();

        modelBuilder.Entity<UserSettings>()
            .Navigation(us => us.SelectedCurrency)
            .AutoInclude();

        base.OnModelCreating(modelBuilder);
    }
}

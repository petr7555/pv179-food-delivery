using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Data;

public class BasicIdentityDbContext : IdentityDbContext
{
    public BasicIdentityDbContext(DbContextOptions<BasicIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var adminRoleId = Guid.NewGuid().ToString();
        var customerRoleId = Guid.NewGuid().ToString();
        var contentManagerRoleId = Guid.NewGuid().ToString();

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole("Admin")
            {
                Id = adminRoleId,
                NormalizedName = "ADMIN",
            },
            new IdentityRole("Customer")
            {
                Id = customerRoleId,
                NormalizedName = "CUSTOMER",
            },
            new IdentityRole("ContentManager")
            {
                Id = contentManagerRoleId,
                NormalizedName = "CONTENTMANAGER",
            }
        );

        var hasher = new PasswordHasher<IdentityUser>();

        // create admin
        var adminId = Guid.NewGuid().ToString();
        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = adminId,
            UserName = "admin@example.com",
            NormalizedUserName = "ADMIN@EXAMPLE.COM",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            PasswordHash = hasher.HashPassword(null, "pass"),
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = adminId,
                RoleId = adminRoleId,
            }
        );

        // create customer
        var customerId = Guid.NewGuid().ToString();
        Console.WriteLine("Generated customerId: "+customerId);
        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = customerId,
            UserName = "customer@example.com",
            NormalizedUserName = "CUSTOMER@EXAMPLE.COM",
            Email = "customer@example.com",
            NormalizedEmail = "CUSTOMER@EXAMPLE.COM",
            PasswordHash = hasher.HashPassword(null, "pass"),
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = customerId,
                RoleId = customerRoleId,
            }
        );

        // create content manager
        var contentManagerId = Guid.NewGuid().ToString();
        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = contentManagerId,
            UserName = "cm@example.com",
            NormalizedUserName = "CM@EXAMPLE.COM",
            Email = "cm@example.com",
            NormalizedEmail = "CM@EXAMPLE.COM",
            PasswordHash = hasher.HashPassword(null, "pass"),
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = contentManagerId,
                RoleId = contentManagerRoleId,
            }
        );
    }
}

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
        var contentManagerRoleId = Guid.NewGuid().ToString();

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole("Admin")
            {
                Id = adminRoleId,
                NormalizedName = "ADMIN",
            },
            new IdentityRole("User")
            {
                NormalizedName = "USER",
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
            UserName = "admin@localhost",
            NormalizedUserName = "ADMIN@LOCALHOST",
            Email = "admin@localhost",
            NormalizedEmail = "ADMIN@LOCALHOST",
            PasswordHash = hasher.HashPassword(null, "pass"),
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = adminId,
                RoleId = adminRoleId,
            }
        );

        // create content manager
        var contentManagerId = Guid.NewGuid().ToString();
        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = contentManagerId,
            UserName = "cm@localhost",
            NormalizedUserName = "CM@LOCALHOST",
            Email = "cm@localhost",
            NormalizedEmail = "CM@LOCALHOST",
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

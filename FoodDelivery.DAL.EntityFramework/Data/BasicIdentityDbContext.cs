using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Data;

public class BasicIdentityDbContext : IdentityDbContext
{
    public BasicIdentityDbContext(DbContextOptions<BasicIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole("Admin")
            {
                NormalizedName = "ADMIN",
            },
            new IdentityRole("ContentManager")
            {
                NormalizedName = "CONTENTMANAGER",
            },
            new IdentityRole("User")
            {
                NormalizedName = "USER",
            }
        );
    }
}

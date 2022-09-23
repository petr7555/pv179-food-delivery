using FoodDeliveryDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryDAL.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, Note = "Extra ham, please" },
            new Order { Id = 2, Note = "Vegan version, thanks" }
        );
    }
}

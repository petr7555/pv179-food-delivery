using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.BL.Services;

public class DbUtilsService : IDbUtilsService
{
    private readonly DbContext _context;

    public DbUtilsService(DbContext context)
    {
        _context = context;
    }

    public void ResetDatabase()
    {
        Console.WriteLine("Resetting database...");
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
}

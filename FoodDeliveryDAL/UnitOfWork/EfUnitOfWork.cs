using FoodDeliveryDAL.Data;
using FoodDeliveryDAL.Models;
using FoodDeliveryDAL.Repositories;

namespace FoodDeliveryDAL.UnitOfWork;

public class EfUnitOfWork : IUnitOfWork
{
    private FoodDeliveryDbContext _context;

    public IRepository<User, int> UserRepository { get; }
    public IRepository<Order, int> OrderRepository { get; }
    public IRepository<Restaurant, int> RestaurantRepository { get; }

    public EfUnitOfWork()
    {
        _context = new FoodDeliveryDbContext();

        UserRepository = new EfRepository<User, int>(_context);
        OrderRepository = new EfRepository<Order, int>(_context);
        RestaurantRepository = new EfRepository<Restaurant, int>(_context);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}

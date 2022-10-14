using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Query;
using FoodDelivery.Infrastructure.EntityFramework.Repositories;
using FoodDelivery.Infrastructure.Query;
using FoodDelivery.Infrastructure.Repository;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly FoodDeliveryDbContext _context;

    public IRepository<User, int> UserRepository { get; }
    public IRepository<Order, int> OrderRepository { get; }
    public IRepository<Restaurant, int> RestaurantRepository { get; }

    public IQuery<Restaurant> RestaurantQuery { get; }

    public EfUnitOfWork()
    {
        _context = new FoodDeliveryDbContext();

        UserRepository = new EfRepository<User, int>(_context);
        OrderRepository = new EfRepository<Order, int>(_context);
        RestaurantRepository = new EfRepository<Restaurant, int>(_context);

        RestaurantQuery = new EfQuery<Restaurant>(_context);
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

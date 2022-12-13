using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Repositories;
using FoodDelivery.Infrastructure.Repository;
using FoodDelivery.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public IRepository<User, Guid> UserRepository { get; }
    public IRepository<Order, Guid> OrderRepository { get; }
    public IRepository<Product, Guid> ProductRepository { get; }
    public IRepository<OrderProduct, Guid> OrderProductRepository { get; }
    public IRepository<Restaurant, Guid> RestaurantRepository { get; }
    public IRepository<Category, Guid> CategoryRepository { get; }

    public EfUnitOfWork(DbContext context)
    {
        _context = context;

        UserRepository = new EfRepository<User, Guid>(_context);
        OrderRepository = new EfRepository<Order, Guid>(_context);
        ProductRepository = new EfRepository<Product, Guid>(_context);
        OrderProductRepository = new EfRepository<OrderProduct, Guid>(_context);
        RestaurantRepository = new EfRepository<Restaurant, Guid>(_context);
        CategoryRepository = new EfRepository<Category, Guid>(_context);
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

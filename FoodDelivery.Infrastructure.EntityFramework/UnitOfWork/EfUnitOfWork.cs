using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Query;
using FoodDelivery.Infrastructure.EntityFramework.Repositories;
using FoodDelivery.Infrastructure.Query;
using FoodDelivery.Infrastructure.Repository;
using FoodDelivery.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public IRepository<User, int> UserRepository { get; }
    public IRepository<Order, int> OrderRepository { get; }
    public IRepository<Restaurant, int> RestaurantRepository { get; }
    public IRepository<Product, int> ProductRepository { get; }
    public IRepository<Category, int> CategoryRepository { get; }

    public IQuery<User> UserQuery { get; }
    public IQuery<Order> OrderQuery { get; }
    public IQuery<Restaurant> RestaurantQuery { get; }
    public IQuery<Product> ProductQuery { get; }
    public IQuery<Category> CategoryQuery { get; }

    public EfUnitOfWork(DbContext context)
    {
        _context = context;

        UserRepository = new EfRepository<User, int>(_context);
        OrderRepository = new EfRepository<Order, int>(_context);
        RestaurantRepository = new EfRepository<Restaurant, int>(_context);
        ProductRepository = new EfRepository<Product, int>(_context);
        CategoryRepository = new EfRepository<Category, int>(_context);

        UserQuery = new EfQuery<User>(_context);
        OrderQuery = new EfQuery<Order>(_context);
        RestaurantQuery = new EfQuery<Restaurant>(_context);
        ProductQuery = new EfQuery<Product>(_context);
        CategoryQuery = new EfQuery<Category>(_context);
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

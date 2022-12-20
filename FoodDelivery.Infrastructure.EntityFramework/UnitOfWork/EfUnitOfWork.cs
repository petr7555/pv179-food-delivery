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
    public IRepository<Currency, Guid> CurrencyRepository { get; }
    public IRepository<CustomerDetails, Guid> CustomerDetailsRepository { get; }
    public IRepository<UserSettings, Guid> UserSettingsRepository { get; }
    public IRepository<Coupon, Guid> CouponRepository { get; }

    public EfUnitOfWork(DbContext context)
    {
        _context = context;

        UserRepository = new EfRepository<User, Guid>(_context);
        OrderRepository = new EfRepository<Order, Guid>(_context);
        ProductRepository = new EfRepository<Product, Guid>(_context);
        OrderProductRepository = new EfRepository<OrderProduct, Guid>(_context);
        RestaurantRepository = new EfRepository<Restaurant, Guid>(_context);
        CategoryRepository = new EfRepository<Category, Guid>(_context);
        CurrencyRepository = new EfRepository<Currency, Guid>(_context);
        CustomerDetailsRepository = new EfRepository<CustomerDetails, Guid>(_context);
        UserSettingsRepository = new EfRepository<UserSettings, Guid>(_context);
        CouponRepository = new EfRepository<Coupon, Guid>(_context);
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

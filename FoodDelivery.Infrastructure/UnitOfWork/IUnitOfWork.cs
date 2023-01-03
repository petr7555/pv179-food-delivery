using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Repository;

namespace FoodDelivery.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    public IRepository<Address, Guid> AddressRepository { get; }
    public IRepository<Category, Guid> CategoryRepository { get; }
    public IRepository<Coupon, Guid> CouponRepository { get; }
    public IRepository<Currency, Guid> CurrencyRepository { get; }
    public IRepository<CustomerDetails, Guid> CustomerDetailsRepository { get; }
    public IRepository<OrderProduct, Guid> OrderProductRepository { get; }
    public IRepository<Order, Guid> OrderRepository { get; }
    public IRepository<Product, Guid> ProductRepository { get; }
    public IRepository<Rating, Guid> RatingRepository { get; }
    public IRepository<Restaurant, Guid> RestaurantRepository { get; }
    public IRepository<User, Guid> UserRepository { get; }
    public IRepository<UserSettings, Guid> UserSettingsRepository { get; }
    public IRepository<Price, Guid> PriceRepository { get; }

    public void Commit();
    public Task CommitAsync();
}

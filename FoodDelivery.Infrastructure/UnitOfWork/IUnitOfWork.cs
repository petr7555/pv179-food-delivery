using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Repository;

namespace FoodDelivery.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
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
    public IRepository<Rating, Guid> RatingRepository { get; }

    public Task CommitAsync();
}

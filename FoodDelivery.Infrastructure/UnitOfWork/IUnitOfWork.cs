using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Query;
using FoodDelivery.Infrastructure.Repository;

namespace FoodDelivery.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    public IRepository<User, int> UserRepository { get; }
    public IRepository<Order, int> OrderRepository { get; }
    public IRepository<Restaurant, int> RestaurantRepository { get; }

    public IQuery<User> UserQuery { get; }
    public IQuery<Order> OrderQuery { get; }
    public IQuery<Restaurant> RestaurantQuery { get; }

    public Task CommitAsync();
}

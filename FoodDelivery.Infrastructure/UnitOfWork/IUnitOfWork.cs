using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Repository;

namespace FoodDelivery.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    public IRepository<User, int> UserRepository { get; }
    public IRepository<Order, int> OrderRepository { get; }
    public IRepository<Restaurant, int> RestaurantRepository { get; }
    public Task CommitAsync();
}

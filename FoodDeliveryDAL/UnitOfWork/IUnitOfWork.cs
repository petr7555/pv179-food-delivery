using FoodDeliveryDAL.Models;
using FoodDeliveryDAL.Repositories;

namespace FoodDeliveryDAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    public IRepository<User, int> UserRepository { get; }
    public IRepository<Order, int> OrderRepository { get; }
    public IRepository<Restaurant, int> RestaurantRepository { get; }
    public Task CommitAsync();
}

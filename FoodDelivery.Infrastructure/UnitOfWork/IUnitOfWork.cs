using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Query;
using FoodDelivery.Infrastructure.Repository;

namespace FoodDelivery.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
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

    public Task CommitAsync();
}

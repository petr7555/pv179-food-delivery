namespace FoodDelivery.Infrastructure.Repository;

public interface IRepository<TEntity, in TKey> where TEntity : class where TKey : struct
{
    public Task<TEntity?> GetByIdAsync(TKey id);
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public void Create(TEntity entity);
    public void Update(TEntity entity, IEnumerable<string> propertiesToUpdate);
    public void Delete(TKey id);
}

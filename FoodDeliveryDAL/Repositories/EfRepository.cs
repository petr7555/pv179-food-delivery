using FoodDeliveryDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryDAL.Repositories;

public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity where TKey : struct

{
    private readonly DbSet<TEntity> _dbSet;

    public EfRepository(DbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public void Create(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TKey id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
}

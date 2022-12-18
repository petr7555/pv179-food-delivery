using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.Repositories;

public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity where TKey : struct
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public EfRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public void Create(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity, IEnumerable<string> propertiesToUpdate)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var originalEntity = _dbSet.Find(entity.Id);
        if (originalEntity == null)
        {
            throw new Exception("Cannot update entity, entity not found.");
        }

        foreach (var property in propertiesToUpdate)
        {
            var newValue = entity.GetType().GetProperty(property)?.GetValue(entity);
            originalEntity.GetType().GetProperty(property)?.SetValue(originalEntity, newValue);
        }
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

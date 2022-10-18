using System.Linq.Expressions;

namespace FoodDelivery.Infrastructure.Query;

/// <summary>
/// Expressions will be applied in the order Where –> OrderBy –> Page
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IQuery<TEntity>
{
    IQuery<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    IQuery<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector, bool descending = false);
    IQuery<TEntity> Page(int pageToFetch, int pageSize = 10);
    IEnumerable<TEntity> Execute();
}

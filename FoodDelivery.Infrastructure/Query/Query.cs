using System.Linq.Expressions;

namespace FoodDelivery.Infrastructure.Query;

public abstract class Query<TEntity> : IQuery<TEntity>
{
    protected List<Expression<Func<TEntity, bool>>> WherePredicates { get; } = new();
    protected (Expression<Func<TEntity, object>> keySelector, bool descending) OrderByConfig { get; private set; }
    protected (int pageToFetch, int pageSize) PageConfig { get; private set; }

    public IQuery<TEntity> Page(int pageToFetch, int pageSize = 10)
    {
        PageConfig = (pageToFetch, pageSize);
        return this;
    }

    public IQuery<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector, bool descending = false)
    {
        OrderByConfig = (keySelector, descending);
        return this;
    }

    public IQuery<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        WherePredicates.Add(predicate);
        return this;
    }

    public abstract IEnumerable<TEntity> Execute();
}

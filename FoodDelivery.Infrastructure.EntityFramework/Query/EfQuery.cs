using FoodDelivery.Infrastructure.Query;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.Query;

public class EfQuery<TEntity> : Query<TEntity> where TEntity : class
{
    private DbContext Context { get; }

    public EfQuery(DbContext context)
    {
        Context = context;
    }

    public override async Task<IEnumerable<TEntity>> ExecuteAsync()
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        query = ApplyWhere(query);
        query = ApplyOrderBy(query);
        query = ApplyPaging(query);

        return await query.ToListAsync();
    }

    private IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query)
    {
        return WherePredicates.Aggregate(query, (current, expr) => current.Where(expr));
    }

    private IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query)
    {
        if (!OrderByConfig.HasValue)
        {
            return query;
        }

        var (keySelector, descending) = OrderByConfig.Value;
        return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }

    private IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query)
    {
        if (!PageConfig.HasValue)
        {
            return query;
        }

        var (pageToFetch, pageSize) = PageConfig.Value;
        return query.Skip((pageToFetch - 1) * pageSize).Take(pageSize);
    }
}

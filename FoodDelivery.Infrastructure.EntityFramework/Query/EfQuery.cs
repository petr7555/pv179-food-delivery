using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.Infrastructure.Query;

namespace FoodDelivery.Infrastructure.EntityFramework.Query;

public class EfQuery<TEntity> : Query<TEntity> where TEntity : class
{
    private FoodDeliveryDbContext Context { get; }

    public EfQuery()
    {
        Context = new FoodDeliveryDbContext();
    }

    public override IEnumerable<TEntity> Execute()
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        if (PageConfig != (0, 0))
        {
            query = ApplyPaging(query);
        }

        if (WherePredicates.Capacity != 0)
        {
            query = ApplyWhere(query);
        }

        if (OrderByConfig != (null, false))
        {
            query = ApplyOrderBy(query);
        }

        return query.ToList();
    }

    private IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query)
    {
        return WherePredicates.Aggregate(query, (current, expr) => current.Where(expr));
    }

    private IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query)
    {
        var (keySelector, descending) = OrderByConfig;
        return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }

    private IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query)
    {
        var (pageToFetch, pageSize) = PageConfig;
        return query.Skip(pageToFetch * pageSize).Take(pageSize);
    }
}

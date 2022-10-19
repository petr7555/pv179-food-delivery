using System.Linq.Expressions;
using FoodDelivery.Infrastructure.Query;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.Query;

public class EfQuery<TEntity> : Query<TEntity> where TEntity : class
{
    private DbContext Context { get; }

    public EfQuery(DbContext dbContext)
    {
        Context = dbContext;
    }

    public override IEnumerable<TEntity> Execute()
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        query = ApplyWhere(query);
        query = ApplyOrderBy(query);
        query = ApplyPaging(query);

        return query.ToList();
    }

    private IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query)
    {
        // THIS
        foreach (var wherePredicate in WherePredicates)
        {
            var body = (dynamic)wherePredicate.Body;
            var propName = "";
            try
            {
                propName = body.Object.Member.Name;
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                propName = body.Left.Member.Name;
            }
            catch (Exception e)
            {
                // ignored
            }
            
            if (propName == "")
            {
                throw new NotImplementedException();
            }
            
            Console.WriteLine(propName);
        }
        // END

        return WherePredicates.Aggregate(query, (current, expr) => current.Where(expr));
    }

    private IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query)
    {
        if (!OrderByConfig.HasValue) return query;

        // THIS
        var (keySelector, descending) = OrderByConfig.Value;
        var propName = keySelector.Body switch
        {
            MemberExpression m => m.Member.Name,
            UnaryExpression { Operand: MemberExpression m } => m.Member.Name,
            _ => throw new NotImplementedException(keySelector.GetType().ToString())
        };
        Console.WriteLine(propName);
        // END

        return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }

    private IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query)
    {
        if (!PageConfig.HasValue) return query;

        var (pageToFetch, pageSize) = PageConfig.Value;
        return query.Skip((pageToFetch - 1) * pageSize).Take(pageSize);
    }
}

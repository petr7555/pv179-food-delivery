using System.Linq.Expressions;

namespace FoodDelivery.BL.DTOs;

public class QueryDto<TDto>
{
    public List<Expression<Func<TDto, bool>>> WherePredicates { get; } = new();
    public (Expression<Func<TDto, object>> keySelector, bool descending)? OrderByConfig { get; private set; }
    public (int pageToFetch, int pageSize)? PageConfig { get; private set; }

    public QueryDto<TDto> Where(Expression<Func<TDto, bool>> predicate)
    {
        WherePredicates.Add(predicate);
        return this;
    }

    public QueryDto<TDto> OrderBy(Expression<Func<TDto, object>> keySelector, bool descending = false)
    {
        OrderByConfig = (keySelector, descending);
        return this;
    }

    public QueryDto<TDto> Page(int pageToFetch, int pageSize = 10)
    {
        PageConfig = (pageToFetch, pageSize);
        return this;
    }
}

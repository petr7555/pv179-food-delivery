using System.Linq.Expressions;
using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.Infrastructure.Query;

namespace FoodDelivery.BL.QueryObject;

public class QueryObject<TDto, TEntity> : IQueryObject<TDto, TEntity> where TEntity : class
{
    private readonly IMapper _mapper;
    private readonly Func<IQuery<TEntity>> _queryFactory;

    public QueryObject(IMapper mapper, Func<IQuery<TEntity>> queryFactory)
    {
        _mapper = mapper;
        _queryFactory = queryFactory;
    }

    public async Task<IEnumerable<TDto>> ExecuteAsync(QueryDto<TDto> queryDto)
    {
        var query = _queryFactory();

        ApplyWhere(query, queryDto);
        ApplyOrderBy(query, queryDto);
        ApplyPaging(query, queryDto);

        return _mapper.Map<IEnumerable<TDto>>(await query.ExecuteAsync());
    }

    private void ApplyWhere(IQuery<TEntity> query, QueryDto<TDto> queryDto)
    {
        foreach (var wherePredicate in queryDto.WherePredicates)
        {
            query.Where(_mapper.Map<Expression<Func<TEntity, bool>>>(wherePredicate));
        }
    }

    private void ApplyOrderBy(IQuery<TEntity> query, QueryDto<TDto> queryDto)
    {
        if (!queryDto.OrderByConfig.HasValue) return;

        var (keySelector, descending) = queryDto.OrderByConfig.Value;
        query.OrderBy(_mapper.Map<Expression<Func<TEntity, object>>>(keySelector), descending);
    }

    private void ApplyPaging(IQuery<TEntity> query, QueryDto<TDto> queryDto)
    {
        if (!queryDto.PageConfig.HasValue) return;

        var (pageToFetch, pageSize) = queryDto.PageConfig.Value;
        query.Page(pageToFetch, pageSize);
    }
}

using System.Linq.Expressions;
using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.Infrastructure.Query;

namespace FoodDelivery.BL.QueryObject;

public class QueryObject<TDto, TEntity> where TEntity : class
{
    private readonly IMapper _mapper;
    private readonly IQuery<TEntity> _query;

    public QueryObject(IMapper mapper, IQuery<TEntity> query)
    {
        _mapper = mapper;
        _query = query;
    }

    public IEnumerable<TDto> Execute(QueryDto<TDto> queryDto)
    {
        ApplyWhere(queryDto);
        ApplyOrderBy(queryDto);
        ApplyPaging(queryDto);

        return _mapper.Map<IEnumerable<TDto>>(_query.Execute());
    }

    private void ApplyWhere(QueryDto<TDto> queryDto)
    {
        foreach (var wherePredicate in queryDto.WherePredicates)
        {
            _query.Where(_mapper.Map<Expression<Func<TEntity, bool>>>(wherePredicate));
        }
    }

    private void ApplyOrderBy(QueryDto<TDto> queryDto)
    {
        if (!queryDto.OrderByConfig.HasValue) return;

        var (keySelector, descending) = queryDto.OrderByConfig.Value;
        _query.OrderBy(_mapper.Map<Expression<Func<TEntity, object>>>(keySelector), descending);
    }

    private void ApplyPaging(QueryDto<TDto> queryDto)
    {
        if (!queryDto.PageConfig.HasValue) return;

        var (pageToFetch, pageSize) = queryDto.PageConfig.Value;
        _query.Page(pageToFetch, pageSize);
    }
}

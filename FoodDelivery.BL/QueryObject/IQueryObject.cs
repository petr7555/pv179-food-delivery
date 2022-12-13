using FoodDelivery.BL.DTOs;

namespace FoodDelivery.BL.QueryObject;

public interface IQueryObject<TDto, TEntity> where TEntity : class
{
    public Task<IEnumerable<TDto>> ExecuteAsync(QueryDto<TDto> queryDto);
}

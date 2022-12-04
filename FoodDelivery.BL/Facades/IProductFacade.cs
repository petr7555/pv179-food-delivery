using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;

namespace FoodDelivery.BL.Facades;

public interface IProductFacade
{
    public Task<IEnumerable<ProductGetDto>> GetAllAsync();
    
    public Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto);
}

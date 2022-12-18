using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;

namespace FoodDelivery.BL.Facades.ProductFacade;

public interface IProductFacade
{
    public Task<IEnumerable<ProductLocalizedGetDto>> GetAllAsync(string username);

    public Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto);
}

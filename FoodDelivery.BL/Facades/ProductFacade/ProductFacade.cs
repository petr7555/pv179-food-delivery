using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.ProductService;

namespace FoodDelivery.BL.Facades.ProductFacade;

public class ProductFacade : IProductFacade
{
    private readonly IProductService _productService;

    public ProductFacade(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IEnumerable<ProductGetDto>> GetAllAsync()
    {
        return await _productService.GetAllAsync();
    }

    public async Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto)
    {
        return await _productService.QueryAsync(queryDto);
    }
}

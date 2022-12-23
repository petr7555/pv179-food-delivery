using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.BL.Services.UserService;

namespace FoodDelivery.BL.Facades.ProductFacade;

public class ProductFacade : IProductFacade
{
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public ProductFacade(IProductService productService, IUserService userService)
    {
        _productService = productService;
        _userService = userService;
    }

    public async Task<IEnumerable<ProductLocalizedGetDto>> GetAllAsync(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        var currency = user.UserSettings.SelectedCurrency;
        var products = await _productService.GetAllAsync();
        var productsLocalized = products.Select(p =>
            new ProductLocalizedGetDto
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Category = p.Category,
                Restaurant = p.Restaurant,
                PricePerEach = p.Prices.Single(price => price.Currency.Id == currency.Id),
            });
        return productsLocalized;
    }

    public async Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto)
    {
        return await _productService.QueryAsync(queryDto);
    }
}

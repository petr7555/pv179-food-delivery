using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.PriceService;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.ProductFacade;

public class ProductFacade : IProductFacade
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPriceService _priceService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public ProductFacade(IUnitOfWork unitOfWork, IPriceService priceService, IProductService productService, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _priceService = priceService;
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

    public async Task<ProductUpdateDto> GetByIdAsyncAsUpdateDto(Guid productId)
    {
        return await _productService.GetByIdAsyncAsUpdateDto(productId);
    }

    public async Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto)
    {
        return await _productService.QueryAsync(queryDto);
    }

    public async Task Create(ProductCreateDto product)
    {
        _productService.Create(product);
        await _unitOfWork.CommitAsync();
    }

    public async Task CreateWithNewPrices(ProductUpdateDto productUpdateDto, List<PriceUpdateDto> priceUpdateDtos)
    {
        _productService.Create(productUpdateDto);
        foreach (var price in priceUpdateDtos)
        {
            _priceService.Create(price);
        }
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(ProductUpdateDto product, List<PriceUpdateDto> prices)
    {
        foreach (var price in prices)
        {
            _priceService.Update(price, new[] { nameof(Price.Amount) });
        }

        _productService.Update(
            product,
            new [] 
            { 
                nameof(Product.Name),
                nameof(Product.Description),
                nameof(Product.ImageUrl),
                nameof(Product.CategoryId)
            });        
        
        await _unitOfWork.CommitAsync();
    }
    public void Delete(Guid productId)
    {
        _productService.Delete(productId);
        _unitOfWork.Commit();
    }
}

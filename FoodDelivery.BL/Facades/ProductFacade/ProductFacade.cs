using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.PriceService;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.ProductFacade;

public class ProductFacade : IProductFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPriceService _priceService;
    private readonly IProductService _productService;

    public ProductFacade(IUnitOfWork unitOfWork, IProductService productService, IPriceService priceService)
    {
        _unitOfWork = unitOfWork;
        _productService = productService;
        _priceService = priceService;
    }

    public async Task<IEnumerable<ProductGetDto>> GetAllAsync()
    {
        return await _productService.GetAllAsync();
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

    public async Task Create(ProductUpdateDto product)
    {
        _productService.Create(product);
        await _unitOfWork.CommitAsync();
    }

    public async Task Update(ProductUpdateDto product)
    {
        _productService.Update(product);
        Console.WriteLine("Price amount in facade: " + product.Price.Amount);
        _priceService.Update(product.Price);
        await _unitOfWork.CommitAsync();
    }
    public void Delete(Guid productId)
    {
        _productService.Delete(productId);
        _unitOfWork.Commit();
    }
}

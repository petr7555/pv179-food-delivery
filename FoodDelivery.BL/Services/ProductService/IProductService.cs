using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.ProductService;

public interface IProductService : ICrudService<Product, Guid, ProductGetDto, ProductCreateDto, ProductUpdateDto>
{
    public Task<ProductUpdateDto> GetByIdAsyncAsUpdateDto(Guid productId);
    public Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto);
    public void Create(ProductUpdateDto product);
    public ProductUpdateDto ConvertToUpdateDto(ProductGetDto product);
}

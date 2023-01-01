using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;

namespace FoodDelivery.BL.Facades.ProductFacade;

public interface IProductFacade
{
    public Task<IEnumerable<ProductGetDto>> GetAllAsync();

    public Task<ProductUpdateDto> GetByIdAsyncAsUpdateDto(Guid productId);

    public Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto);

    public Task Create(ProductCreateDto product);
    public Task Create(ProductUpdateDto product);
    public Task Update(ProductUpdateDto product);
    public void Delete(Guid productId);    
}

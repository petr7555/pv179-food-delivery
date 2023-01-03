using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;

namespace FoodDelivery.BL.Facades.ProductFacade;

public interface IProductFacade
{
    public Task<IEnumerable<ProductLocalizedGetDto>> GetAllAsync(string username);

    public Task<ProductUpdateDto> GetByIdAsyncAsUpdateDto(Guid productId);

    public Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto);

    public Task Create(ProductCreateDto product);
    public Task CreateWithNewPrices(ProductUpdateDto productUpdateDto, List<PriceUpdateDto> priceUpdateDtos);
    public Task UpdateAsync(ProductUpdateDto product, List<PriceUpdateDto> prices);
    public void Delete(Guid productId);
}

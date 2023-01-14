using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;

namespace FoodDelivery.BL.Facades.RestaurantFacade;

public interface IRestaurantFacade
{
    public Task<IEnumerable<RestaurantGetDto>> GetAllAsync();

    public Task<RestaurantGetDto> GetByIdAsync(Guid restaurantId);

    public Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto);

    public Task CreateAsync(RestaurantCreateDto restaurantCreateDto);

    public Task CreateWithNewPrices(RestaurantCreateDto restaurantCreateDto,
        IEnumerable<PriceCreateDto> priceCreateDtos);

    public Task UpdateAsync(RestaurantCreateDto restaurantCreateDto, List<PriceCreateDto> priceCreateDtos);
    public Task Delete(Guid restaurantId);

    public RestaurantCreateDto ConvertToCreateDto(RestaurantGetDto restaurantGetDto);
}

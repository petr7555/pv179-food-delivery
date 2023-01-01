using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;

namespace FoodDelivery.BL.Facades.RestaurantFacade;

public interface IRestaurantFacade
{
    public Task<IEnumerable<RestaurantGetDto>> GetAllAsync();

    public Task<RestaurantGetDto> GetById(Guid id);

    public Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto);

    public Task CreateAsync(RestaurantCreateDto restaurantCreateDto);
    public Task CreateWithNewPrice(RestaurantCreateDto restaurantCreateDto, PriceCreateDto priceCreateDto);
}

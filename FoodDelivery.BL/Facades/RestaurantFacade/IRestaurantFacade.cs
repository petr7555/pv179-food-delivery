using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;

namespace FoodDelivery.BL.Facades.RestaurantFacade;

public interface IRestaurantFacade
{
    public Task<IEnumerable<RestaurantGetDto>> GetAllAsync();

    public Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto);

    public Task CreateAsync(RestaurantCreateDto restaurantCreateDto);
}

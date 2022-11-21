using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;

namespace FoodDelivery.BL.Facades;

public interface IRestaurantFacade
{
    public Task<IEnumerable<RestaurantGetDto>> GetAllAsync();

    public Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto);

    public Task Create(RestaurantCreateDto restaurantCreateDto);
}

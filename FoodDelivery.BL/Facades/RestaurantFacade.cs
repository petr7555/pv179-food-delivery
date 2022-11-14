using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services;

namespace FoodDelivery.BL.Facades;

public class RestaurantFacade: IRestaurantFacade
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantFacade(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }
    
    public async Task<IEnumerable<RestaurantGetDto>> GetAllAsync()
    {
        return await _restaurantService.GetAllAsync();
    }

    public async Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto)
    {
        return await _restaurantService.QueryAsync(queryDto);
    }
}

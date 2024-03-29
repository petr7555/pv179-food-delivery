using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.RestaurantService;

public interface
    IRestaurantService : ICrudService<Restaurant, Guid, RestaurantGetDto, RestaurantCreateDto, RestaurantUpdateDto>
{
    public Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto);

    public void Update(RestaurantCreateDto restaurantCreateDto);

    public RestaurantCreateDto ConvertToCreateDto(RestaurantGetDto restaurantGetDto);
}

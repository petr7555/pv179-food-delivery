using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services;

public interface IRestaurantService: ICrudService<Restaurant, int, RestaurantGetDto, RestaurantCreateDto, RestaurantUpdateDto>
{
    public Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto);
}

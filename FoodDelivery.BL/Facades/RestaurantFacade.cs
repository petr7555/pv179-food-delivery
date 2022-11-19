using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades;

public class RestaurantFacade : IRestaurantFacade
{
    private readonly IRestaurantService _restaurantService;
    private readonly IUnitOfWork _uow;

    public RestaurantFacade(IUnitOfWork uow, IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
        _uow = uow;
    }

    public async Task<IEnumerable<RestaurantGetDto>> GetAllAsync()
    {
        return await _restaurantService.GetAllAsync();
    }

    public async Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto)
    {
        return await _restaurantService.QueryAsync(queryDto);
    }

    public async Task Create(RestaurantCreateDto dto)
    {
        _restaurantService.Create(dto);
        await _uow.CommitAsync();
    }
}

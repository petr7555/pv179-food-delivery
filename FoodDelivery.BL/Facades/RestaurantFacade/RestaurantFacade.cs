using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services.RestaurantService;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.RestaurantFacade;

public class RestaurantFacade : IRestaurantFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRestaurantService _restaurantService;

    public RestaurantFacade(IUnitOfWork unitOfWork, IRestaurantService restaurantService)
    {
        _unitOfWork = unitOfWork;
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

    public async Task CreateAsync(RestaurantCreateDto dto)
    {
        _restaurantService.Create(dto);
        await _unitOfWork.CommitAsync();
    }
}

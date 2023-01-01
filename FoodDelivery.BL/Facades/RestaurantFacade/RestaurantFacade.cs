using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services.PriceService;
using FoodDelivery.BL.Services.RestaurantService;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.RestaurantFacade;

public class RestaurantFacade : IRestaurantFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRestaurantService _restaurantService;
    private readonly IPriceService _priceService;

    public RestaurantFacade(IUnitOfWork unitOfWork, IRestaurantService restaurantService, IPriceService priceService)
    {
        _unitOfWork = unitOfWork;
        _restaurantService = restaurantService;
        _priceService = priceService;
    }

    public async Task<RestaurantGetDto> GetById(Guid id)
    {
        return await _restaurantService.GetByIdAsync(id);
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

    public async Task CreateWithNewPrice(RestaurantCreateDto restaurantCreateDto, PriceCreateDto priceCreateDto)
    {
        _priceService.Create(priceCreateDto);
        restaurantCreateDto.DeliveryPriceId = priceCreateDto.Id;
        _restaurantService.Create(restaurantCreateDto);
        await _unitOfWork.CommitAsync();
    }
}

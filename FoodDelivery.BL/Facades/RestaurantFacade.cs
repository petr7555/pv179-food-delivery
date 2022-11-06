using AutoMapper;
using FoodDelivery.BL.Configs;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services;
using FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;

namespace FoodDelivery.BL.Facades;

public class RestaurantFacade
{
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

    public async Task<IEnumerable<RestaurantGetDto>> GetAllAsync()
    {
        await using (var uow = new EfUnitOfWork())
        {
            var restaurantService = new RestaurantService(uow, _mapper);
            return await restaurantService.GetAllAsync();
        }
    }

    public async Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto)
    {
        await using (var uow = new EfUnitOfWork())
        {
            var restaurantService = new RestaurantService(uow, _mapper);
            return restaurantService.QueryAsync(queryDto);
        }
    }
}

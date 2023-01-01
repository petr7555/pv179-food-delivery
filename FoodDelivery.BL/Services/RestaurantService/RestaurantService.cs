using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.RestaurantService;

public class
    RestaurantService : CrudService<Restaurant, Guid, RestaurantGetDto, RestaurantCreateDto, RestaurantUpdateDto>,
        IRestaurantService
{
    private readonly IQueryObject<RestaurantGetDto, Restaurant> _queryObject;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper,
        IQueryObject<RestaurantGetDto, Restaurant> queryObject) : base(unitOfWork.RestaurantRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }
}

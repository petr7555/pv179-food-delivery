using AutoMapper;
using FoodDelivery.BL.DTOs;
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
    private readonly IUnitOfWork _unitOfWork;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.RestaurantRepository, mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto)
    {
        var queryObject = new QueryObject<RestaurantGetDto, Restaurant>(Mapper, _unitOfWork.RestaurantQuery);
        return await queryObject.ExecuteAsync(queryDto);
    }
}

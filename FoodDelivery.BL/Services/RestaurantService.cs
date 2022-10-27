using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services;

public class RestaurantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantGetDto>> GetAllAsync()
    {
        var restaurants = await _unitOfWork.RestaurantRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RestaurantGetDto>>(restaurants);
    }

    public IEnumerable<RestaurantGetDto> QueryAsync(QueryDto<RestaurantGetDto> queryDto)
    {
        var queryObject = new QueryObject<RestaurantGetDto, Restaurant>(_mapper, _unitOfWork.RestaurantQuery);
        return queryObject.Execute(queryDto);
    }
}

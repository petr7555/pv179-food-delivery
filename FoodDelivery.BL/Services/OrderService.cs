using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services;

public class OrderService : CrudService<Order, int, OrderGetDto, OrderCreateDto, OrderUpdateDto>, IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.OrderRepository, mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto)
    {
        var queryObject = new QueryObject<OrderGetDto, Order>(Mapper, _unitOfWork.OrderQuery);
        return await queryObject.ExecuteAsync(queryDto);
    }
}

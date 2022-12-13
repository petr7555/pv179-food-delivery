using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.OrderService;

public class OrderService : CrudService<Order, Guid, OrderGetDto, OrderCreateDto, OrderUpdateDto>, IOrderService
{
    private readonly IQueryObject<OrderGetDto, Order> _queryObject;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<OrderGetDto, Order> queryObject) : base(
        unitOfWork.OrderRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }
}

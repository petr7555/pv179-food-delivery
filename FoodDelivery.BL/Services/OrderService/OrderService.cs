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

    public async Task FulfillOrderAsync(Guid orderId)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
        order.OrderStatus = OrderStatus.Paid;
        _unitOfWork.OrderRepository.Update(order);
        await _unitOfWork.CommitAsync();
    }
}

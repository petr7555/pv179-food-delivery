using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Services;

namespace FoodDelivery.BL.Facades;

public class OrderFacade
{
    private readonly IOrderService _orderService;

    public OrderFacade(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IEnumerable<OrderGetDto>> GetAllAsync()
    {
        return await _orderService.GetAllAsync();
    }

    public async Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto)
    {
        return await _orderService.QueryAsync(queryDto);
    }
}

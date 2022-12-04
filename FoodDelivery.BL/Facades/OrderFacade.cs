using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Services.OrderService;
using FoodDelivery.BL.Services.UserService;

namespace FoodDelivery.BL.Facades;

public class OrderFacade : IOrderFacade
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;

    public OrderFacade(IOrderService orderService, IUserService userService)
    {
        _orderService = orderService;
        _userService = userService;
    }

    public async Task<IEnumerable<OrderGetDto>> GetAllAsync()
    {
        return await _orderService.GetAllAsync();
    }

    public async Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto)
    {
        return await _orderService.QueryAsync(queryDto);
    }

    public async Task<IEnumerable<OrderGetDto>> GetOrdersForUserAsync(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        var orders = await _orderService.QueryAsync(new QueryDto<OrderGetDto>().Where(o => o.CustomerDetails.Customer.Id == user.Id));
        return orders;
    }
}

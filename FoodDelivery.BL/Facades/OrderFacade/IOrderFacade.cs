using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;

namespace FoodDelivery.BL.Facades.OrderFacade;

public interface IOrderFacade
{
    public Task<IEnumerable<OrderGetDto>> GetAllAsync();

    public Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto);

    public Task<IEnumerable<OrderGetDto>> GetOrdersForUserAsync(string username);

    public Task AddToCartAsync(string username, Guid productId);

    public Task<OrderGetDto?> GetActiveOrder(string username);
}

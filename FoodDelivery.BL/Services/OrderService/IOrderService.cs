using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.OrderService;

public interface IOrderService : ICrudService<Order, Guid, OrderGetDto, OrderCreateDto, OrderUpdateDto>
{
    public Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto);
}

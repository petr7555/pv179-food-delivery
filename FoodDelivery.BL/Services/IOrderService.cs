using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services
{
    public interface IOrderService : ICrudService<Order, int, OrderGetDto, OrderCreateDto, OrderUpdateDto>
    {
        public IEnumerable<OrderGetDto> QueryAsync(QueryDto<OrderGetDto> queryDto);
    }
}
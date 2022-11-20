using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Order;

namespace FoodDelivery.BL.Facades
{
    public interface IOrderFacade
    {
        public Task<IEnumerable<OrderGetDto>> GetAllAsync();

        public Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto);
    }
}

using AutoMapper;
using FoodDelivery.BL.Configs;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.Services;
using FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;
using FoodDelivery.BL.DTOs.Order;

namespace FoodDelivery.BL.Facades
{
    public class OrderFacade
    {
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        private readonly IOrderService _orderService;

        public OrderFacade(IOrderService iOrderService)
        {
            _orderService = iOrderService;
        }

        public async Task<IEnumerable<OrderGetDto>> GetAllAsync()
        {
             return await _orderService.GetAllAsync();
        }

        public async Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto)
        {
            return _orderService.QueryAsync(queryDto);
        }
    }
}
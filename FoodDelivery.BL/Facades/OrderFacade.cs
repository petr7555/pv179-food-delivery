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

        public async Task<IEnumerable<OrderGetDto>> GetAllAsync()
        {
            await using (var uow = new EfUnitOfWork())
            {
                var orderService = new OrderService(uow, _mapper);
                return await orderService.GetAllAsync();
            }
        }

        public async Task<IEnumerable<OrderGetDto>> QueryAsync(QueryDto<OrderGetDto> queryDto)
        {
            await using (var uow = new EfUnitOfWork())
            {
                var orderService = new OrderService(uow, _mapper);
                return orderService.QueryAsync(queryDto);
            }
        }
    }
}

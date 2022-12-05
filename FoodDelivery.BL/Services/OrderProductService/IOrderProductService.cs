using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.OrderProductService;

public interface IOrderProductService : ICrudService<OrderProduct, Guid, OrderProductGetDto, OrderProductCreateDto,
    OrderProductUpdateDto>
{
}

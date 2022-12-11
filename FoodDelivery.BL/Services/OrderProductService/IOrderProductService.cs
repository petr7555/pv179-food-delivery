using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.OrderProductService;

public interface IOrderProductService : ICrudService<OrderProduct, Guid, OrderProductGetDto, OrderProductCreateDto,
    OrderProductUpdateDto>
{
    public Task<IEnumerable<OrderProductGetDto>> QueryAsync(QueryDto<OrderProductGetDto> queryDto);

    Task<IEnumerable<ProductGetDto>> GetProductsForOrderAsync(Guid orderId);
}

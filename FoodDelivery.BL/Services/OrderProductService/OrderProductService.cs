using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.OrderProductService;

public class OrderProductService :
    CrudService<OrderProduct, Guid, OrderProductGetDto, OrderProductCreateDto, OrderProductUpdateDto>,
    IOrderProductService
{
    private readonly IQueryObject<OrderProductGetDto, OrderProduct> _queryObject;

    public OrderProductService(IUnitOfWork unitOfWork, IMapper mapper,
        IQueryObject<OrderProductGetDto, OrderProduct> queryObject) : base(unitOfWork.OrderProductRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<OrderProductGetDto>> QueryAsync(QueryDto<OrderProductGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }

    public async Task<IEnumerable<ProductGetDto>> GetProductsForOrderAsync(Guid orderId)
    {
        var orderProducts = await QueryAsync(
            new QueryDto<OrderProductGetDto>()
                .Where(op => op.OrderId == orderId)
        );
        return orderProducts.Select(op =>
        {
            var result = op.Product;
            result.Quantity = op.Quantity;
            result.FinalPrice = op.FinalPrice;
            return result;
        });
    }
}

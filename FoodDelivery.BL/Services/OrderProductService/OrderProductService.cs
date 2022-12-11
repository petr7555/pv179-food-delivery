using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;
using FoodDelivery.BL.QueryObject;

namespace FoodDelivery.BL.Services.OrderProductService;

public class OrderProductService :
    CrudService<OrderProduct, Guid, OrderProductGetDto, OrderProductCreateDto, OrderProductUpdateDto>,
    IOrderProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.OrderProductRepository, mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderProductGetDto>> QueryAsync(QueryDto<OrderProductGetDto> queryDto)
    {
        var queryObject = new QueryObject<OrderProductGetDto, OrderProduct>(Mapper, _unitOfWork.OrderProductQuery);
        return await queryObject.ExecuteAsync(queryDto);
    }
    
    public async Task<IEnumerable<ProductGetDto>> GetProductsForOrderAsync(Guid orderId)
    {
        var orderProducts = await QueryAsync(
            new QueryDto<OrderProductGetDto>()
                .Where(op => op.OrderId == orderId)
        );
        return orderProducts.Select(op => op.Product);
    }
}

using AutoMapper;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.OrderProductService;

public class OrderProductService :
    CrudService<OrderProduct, Guid, OrderProductGetDto, OrderProductCreateDto, OrderProductUpdateDto>,
    IOrderProductService
{
    public OrderProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.OrderProductRepository, mapper)
    {
    }
}

using AutoMapper;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services;

public class ProductService : CrudService<Product, int, ProductGetDto, ProductCreateDto, ProductUpdateDto>,
    IProductService
{
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.ProductRepository, mapper)
    {
    }
}

using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services;

public class ProductService : CrudService<Product, int, ProductGetDto, ProductCreateDto, ProductUpdateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.ProductRepository, mapper)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IEnumerable<ProductGetDto> QueryAsync(QueryDto<ProductGetDto> queryDto)
    {
        var queryObject = new QueryObject<ProductGetDto, Product>(Mapper, _unitOfWork.ProductQuery);
        return queryObject.Execute(queryDto);
    }
}
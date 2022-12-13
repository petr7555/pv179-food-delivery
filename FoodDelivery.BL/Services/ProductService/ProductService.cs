using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.ProductService;

public class ProductService : CrudService<Product, Guid, ProductGetDto, ProductCreateDto, ProductUpdateDto>,
    IProductService
{
    private readonly IQueryObject<ProductGetDto, Product> _queryObject;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<ProductGetDto, Product> queryObject) :
        base(unitOfWork.ProductRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }
}

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
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.ProductRepository, mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto)
    {
        var queryObject = new QueryObject<ProductGetDto, Product>(Mapper, _unitOfWork.ProductQuery);
        return await queryObject.ExecuteAsync(queryDto);
    }
}

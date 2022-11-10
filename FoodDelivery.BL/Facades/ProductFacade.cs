using AutoMapper;
using FoodDelivery.BL.Configs;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Services;
using FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;

namespace FoodDelivery.BL.Facades;

public class ProductFacade
{
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

    public async Task<IEnumerable<ProductGetDto>> GetAllAsync()
    {
        await using (var uow = new EfUnitOfWork())
        {
            var productService = new ProductService(uow, _mapper);
            return await productService.GetAllAsync();
        }
    }

    public async Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto)
    {
        await using (var uow = new EfUnitOfWork())
        {
            var productService = new ProductService(uow, _mapper);
            return productService.QueryAsync(queryDto);
        }
    }
}
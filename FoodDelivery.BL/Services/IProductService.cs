using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services;

public interface IProductService: ICrudService<Product, int, ProductGetDto, ProductCreateDto, ProductUpdateDto>
{
    public Task<IEnumerable<ProductGetDto>> QueryAsync(QueryDto<ProductGetDto> queryDto);
}
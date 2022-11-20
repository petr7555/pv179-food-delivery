using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services;

public class CategoryService : CrudService<Category, int, CategoryGetDto, CategoryCreateDto, CategoryUpdateDto>,
    ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.CategoryRepository, mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CategoryGetDto>> QueryAsync(QueryDto<CategoryGetDto> queryDto)
    {
        var queryObject = new QueryObject<CategoryGetDto, Category>(Mapper, _unitOfWork.CategoryQuery);
        return await queryObject.ExecuteAsync(queryDto);
    }

    public async Task<IEnumerable<RestaurantGetDto>> GetAllRestaurants(CategoryGetDto category)
    {
        return (await GetAllAsync())
            .Where(c => c.Name == category.Name)
            .SelectMany(c => c.Products)
            .Select(p => p.Restaurant)
            .GroupBy(r => r.Id)
            .Select(g => Mapper.Map<RestaurantGetDto>(g.First()));
    }
}

using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.CategoryService;

public interface ICategoryService : ICrudService<Category, Guid, CategoryGetDto, CategoryCreateDto, CategoryUpdateDto>
{
    public Task<IEnumerable<CategoryGetDto>> QueryAsync(QueryDto<CategoryGetDto> queryDto);

    public Task<IEnumerable<RestaurantGetDto>> GetRestaurantsForCategoryAsync(Guid categoryId);
}

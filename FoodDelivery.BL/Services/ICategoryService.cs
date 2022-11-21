using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services;

public interface ICategoryService : ICrudService<Category, int, CategoryGetDto, CategoryCreateDto, CategoryUpdateDto>
{
    public Task<IEnumerable<CategoryGetDto>> QueryAsync(QueryDto<CategoryGetDto> queryDto);

    public Task<IEnumerable<RestaurantGetDto>> GetRestaurantsForCategory(int categoryId);
}

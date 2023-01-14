using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.CategoryService;

public class CategoryService : CrudService<Category, Guid, CategoryGetDto, CategoryCreateDto, CategoryUpdateDto>,
    ICategoryService
{
    private readonly IQueryObject<CategoryGetDto, Category> _queryObject;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<CategoryGetDto, Category> queryObject) :
        base(unitOfWork.CategoryRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<CategoryGetDto>> QueryAsync(QueryDto<CategoryGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }

    public async Task<IEnumerable<RestaurantGetDto>> GetRestaurantsForCategoryAsync(Guid categoryId)
    {
        var categories = await GetAllAsync();

        // use given category as a starting point
        var subcategories = new List<CategoryGetDto>() { categories.Single(c => c.Id == categoryId) };

        var index = 0;
        while (index < subcategories.Count())
        {
            // find all subcategories
            var temp = categories.Where(category => category?.ParentCategoryId == subcategories.ElementAt(index).Id);

            // if anything was found, replace parent category with its subcategories. If nothing new, move to the next one.
            if (temp?.Count() > 0)
            {
                subcategories.AddRange(temp);
                subcategories.RemoveAt(index);
            }
            else
            {
                index++;
            }
        }

        var restaurants = new List<Restaurant>();

        // get all restaurants for subcategories
        foreach (var category in subcategories)
        {
            restaurants.AddRange(
                category.Products
                    .Select(product => product.Restaurant)
            );
        }

        // remove duplicates and return
        return restaurants.GroupBy(restaurant => restaurant.Id)
            .Select(group => Mapper.Map<RestaurantGetDto>(group.First()));
    }
}

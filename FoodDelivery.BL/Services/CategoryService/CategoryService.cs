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

        Console.WriteLine("ALL CATEGORIES ********************");
        for (int i = 0; i < categories.Count(); i++)
        {
            Console.Write(categories.ElementAt(i).Name);
            if (categories.ElementAt(i).Products.Count() > 0)
            {
                Console.Write(" " + categories.ElementAt(i).Products.First().Name);
            }
            Console.WriteLine();

        }

        return categories
            .Single(c => c.Id == categoryId)
            .Products
            .Select(p => p.Restaurant)
            .GroupBy(r => r.Id)
            .Select(g => Mapper.Map<RestaurantGetDto>(g.First()));
    }
}

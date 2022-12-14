using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades.RestaurantFacade;
using FoodDelivery.BL.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "Customer, ContentManager")]
public class RestaurantList : PageModel
{
    public string NameSort { get; set; }
    public string CurrentFilter { get; set; }
    public IEnumerable<RestaurantGetDto> Restaurants { get; set; }
    public IEnumerable<CategoryGetDto> Categories { get; set; }
    [BindProperty]
    public string SelectedTag { get; set; }
    public SelectList TagOptions { get; set; }

    private readonly IRestaurantFacade _restaurantFacade;
    private readonly ICategoryService _categoryService;

    public RestaurantList(IRestaurantFacade restaurantFacade, ICategoryService categoryService)
    {
        _restaurantFacade = restaurantFacade;
        _categoryService = categoryService;
    }

    public async Task OnGetAsync(string sortOrder, string searchString, string SelectedTag)
    {
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

        CurrentFilter = searchString;        
        Categories = await _categoryService.GetAllAsync();

        // sort categories by its tree structure, then alphabetically for each layer
        var currentLayer = Categories.Where(category => !category.ParentCategoryId.HasValue);
        var sortedCategories = new List<CategoryGetDto>();
        while (currentLayer.Any()) {
            currentLayer = currentLayer.OrderBy(category => category.Name);
            sortedCategories.AddRange(currentLayer);
            var temp = new List<CategoryGetDto>();

            // find all subcategories
            foreach (var category in currentLayer)
            {                
                temp.AddRange(Categories.Where(ctg => ctg?.ParentCategoryId == category.Id));
            }

            currentLayer = temp;
        }
        
        TagOptions = new SelectList(sortedCategories, nameof(CategoryGetDto.Id), nameof(CategoryGetDto.Name));

        if (SelectedTag != null)
        {
            Restaurants = await _categoryService.GetRestaurantsForCategoryAsync(new Guid(SelectedTag));
        } else
        {
            Restaurants = await _restaurantFacade.GetAllAsync();
        }

        if (!String.IsNullOrEmpty(searchString))
        {
            Restaurants = Restaurants.Where(restaurant => restaurant.Name.ToLower().Contains(searchString.ToLower()));
        }

        Restaurants = Restaurants.OrderBy(restaurant => restaurant.Name);
    }
}

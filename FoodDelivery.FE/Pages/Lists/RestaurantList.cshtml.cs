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
    public int[] SelectedTags { get; set; }
    public SelectList TagOptions { get; set; }

    private readonly IRestaurantFacade _restaurantFacade;
    private readonly ICategoryService _categoryService;

    public RestaurantList(IRestaurantFacade restaurantFacade, ICategoryService categoryService)
    {
        _restaurantFacade = restaurantFacade;
        _categoryService = categoryService;
    }

    public async Task OnGetAsync(string sortOrder, string searchString)
    {
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

        CurrentFilter = searchString;

        Restaurants = await _restaurantFacade.GetAllAsync();
        Categories = await _categoryService.GetAllAsync();
        TagOptions = new SelectList(Categories, nameof(CategoryGetDto.Id), nameof(CategoryGetDto.Name));

        if (!String.IsNullOrEmpty(searchString))
        {
            Restaurants = Restaurants.Where(restaurant => restaurant.Name.Contains(searchString));
        }        
    }
}

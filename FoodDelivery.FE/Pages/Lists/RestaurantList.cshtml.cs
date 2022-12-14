using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades.RestaurantFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "Customer, ContentManager")]
public class RestaurantList : PageModel
{
    public IEnumerable<RestaurantGetDto> Restaurants { get; set; }
    public IEnumerable<CategoryGetDto> Categories { get; set; }
    [BindProperty]
    public string SelectedTag { get; set; }
    public SelectList TagOptions { get; set; }

    private readonly IRestaurantFacade _restaurantFacade;

    public RestaurantList(IRestaurantFacade restaurantFacade)
    {
        _restaurantFacade = restaurantFacade;
    }

    public async Task OnGetAsync(string sortOrder, string searchString, string SelectedTag)
    {
        // Restaurants = await _restaurantFacade.QueryAsync(
        // new QueryDto<RestaurantGetDto>()
        // .Where(r => r.Name.Contains("Pizza"))
        // .OrderBy(r => r.Name));

        Restaurants = await _restaurantFacade.GetAllAsync();
        Categories = await _categoryService.GetAllAsync();        
        TagOptions = new SelectList(Categories, nameof(CategoryGetDto.Id), nameof(CategoryGetDto.Name));
        

        if (!String.IsNullOrEmpty(searchString))
        {
            Restaurants = Restaurants.Where(restaurant => restaurant.Name.Contains(searchString));
        }

        //var test = TagOptions.Where(tag => tag.Selected);

        //if (SelectedTag > 0)
        //{
        //var iterator = TagOptions.Items.GetEnumerator();
        //for (int i = 0; i < SelectedTag; i++)
        //{
        //    iterator.MoveNext();
        //}

        //TagOptions.
        //var item = (SelectListItem)iterator.Current;

        //Restaurants = await _categoryService.GetRestaurantsForCategoryAsync(new Guid(item.Value));

        Console.WriteLine("SELECTED TAG: " + SelectedTag);
        Console.WriteLine(TagOptions.First().Value);

        if (SelectedTag != null
            //new Guid(SelectedTag) != new Guid("00000000-0000-0000-0000-000000000000")
            && !SelectedTag.Equals(Categories.Where((category) => category.Name.Equals("All"))))
        {
            for (int i = 0; i < Categories.Count(); i++)
            {
                Console.Write(Categories.ElementAt(i).Name);
                if (Categories.ElementAt(i).Products.Count() > 0) {
                    Console.Write(" " + Categories.ElementAt(i).Products.First().Name);
                }
                Console.WriteLine();
                
            }
            //Restaurants = await _categoryService.GetRestaurantsForCategoryAsync(new Guid(TagOptions.First().Value));
            Restaurants = await _categoryService.GetRestaurantsForCategoryAsync(new Guid(SelectedTag));
        }            

            //TagOptions.Items.GetEnumerator().
            //if (test.Count() == 0)
            //{
            //    Restaurants = new List<RestaurantGetDto>();
            //}
            //}

        //}
    }
}

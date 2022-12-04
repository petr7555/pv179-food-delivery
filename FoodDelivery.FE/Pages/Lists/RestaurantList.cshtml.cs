using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "Customer, ContentManager")]
public class RestaurantList : PageModel
{
    public IEnumerable<RestaurantGetDto> Restaurants { get; set; }
    private readonly IRestaurantFacade _restaurantFacade;

    public RestaurantList(IRestaurantFacade restaurantFacade)
    {
        _restaurantFacade = restaurantFacade;
    }

    public async Task OnGet()
    {
        // Restaurants = await _restaurantFacade.QueryAsync(
        // new QueryDto<RestaurantGetDto>()
        // .Where(r => r.Name.Contains("Pizza"))
        // .OrderBy(r => r.Name));

        Restaurants = await _restaurantFacade.GetAllAsync();
    }
}

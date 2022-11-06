using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryFE.Pages.Lists;

public class RestaurantList : PageModel
{
    public IEnumerable<RestaurantGetDto> Restaurants { get; set; }

    public async Task OnGet()
    {
        var restaurantFacade = new RestaurantFacade();
        Restaurants = await restaurantFacade.QueryAsync(
        new QueryDto<RestaurantGetDto>()
        .Where(r => r.Name.Contains("Pizza"))
        .OrderBy(r => r.Name));
        
        // Restaurants = await new RestaurantFacade().GetAllAsync();
    }
}

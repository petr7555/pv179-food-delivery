using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms;

[Authorize(Roles = "ContentManager")]
public class AddRestaurant : PageModel
{
    [BindProperty]
    public RestaurantCreateDto Restaurant { get; set; }

    private readonly IRestaurantFacade _restaurantFacade;

    public AddRestaurant(IRestaurantFacade restaurantFacade)
    {
        _restaurantFacade = restaurantFacade;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _restaurantFacade.Create(Restaurant);

        // TODO
        // return Page();
        return RedirectToPage("../Lists/RestaurantList");
    }
}

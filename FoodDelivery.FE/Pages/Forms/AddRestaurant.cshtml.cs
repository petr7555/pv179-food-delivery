using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms
{
    public class AddRestaurant : PageModel
    {
        private readonly IRestaurantFacade _restaurantFacade;

        [BindProperty]
        public RestaurantCreateDto Restaurant { get; set; }

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

            return RedirectToPage("../Lists/RestaurantList");
        }
    }
}

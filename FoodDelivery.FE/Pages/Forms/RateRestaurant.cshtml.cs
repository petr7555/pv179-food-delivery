using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Facades.RatingFacade;
using FoodDelivery.BL.Facades.RestaurantFacade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms
{
    public class RateRestaurant : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public RestaurantGetDto Restaurant { get; set; }

        [BindProperty(SupportsGet = true)]
        public RatingCreateDto Rating { get; set; }
        private Guid OrderId { get; set; }
        public Guid RestaurantId { get; set; }

        private readonly IOrderFacade _orderFacade;
        private readonly IRestaurantFacade _restaurantFacade;
        private readonly IRatingFacade _ratingFacade;

        public RateRestaurant(IOrderFacade orderFacade, IRestaurantFacade restaurantFacade, IRatingFacade ratingFacade)
        {
            _orderFacade = orderFacade;
            _restaurantFacade = restaurantFacade;
            _ratingFacade = ratingFacade;
        }
        public async Task OnGet(Guid restaurantId, Guid orderId)
        {
            OrderId = orderId;
            RestaurantId = restaurantId;
            Rating = new RatingCreateDto();
            Rating.RestaurantId = restaurantId;
            Rating.OrderId = orderId;
            Rating.Restaurant = await _restaurantFacade.GetById(restaurantId);
            Rating.Order = await _orderFacade.GetByIdAsync(orderId);
        }

        public async Task<IActionResult> OnPost()
        {   
            await _ratingFacade.RateRestaurant(Rating);

            return RedirectToPage("/Orders/Details", new { id = Rating.OrderId });
        }
    }
}

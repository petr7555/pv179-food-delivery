using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Facades.RatingFacade;
using FoodDelivery.BL.Facades.RestaurantFacade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Orders;

public class DetailsModel : PageModel
{
    public OrderWithProductsGetDto Order { get; set; } = default!;
    public List<RestaurantGetDto> Restaurants { get; set; }

    private readonly IOrderFacade _orderFacade;
    private readonly IRestaurantFacade _restaurantFacade;
    private readonly IRatingFacade _ratingFacade;

    public DetailsModel(IOrderFacade orderFacade, IRestaurantFacade restaurantFacade, IRatingFacade ratingFacade)
    {
        _orderFacade = orderFacade;
        _restaurantFacade = restaurantFacade;
        _ratingFacade = ratingFacade;
    }

    public async Task<IActionResult> OnGet(Guid id)
    {
        var foundOrder = await _orderFacade.GetByIdAsync(id);
        if (foundOrder == null)
        {
            return NotFound();
        }

        Order = foundOrder;

        var products = Order.Products;
        Restaurants = products.Select((product) => product.Restaurant).Distinct().ToList();

        return Page();
    }

    public async Task OnPostAsync(Guid restaurantId, Guid orderId, int stars, string comment)
    {


    }

    public async Task<bool> AlreadyRated(Guid restaurantId)
    {
        return await _ratingFacade.AlreadyRated(restaurantId, Order.Id);
    }
}

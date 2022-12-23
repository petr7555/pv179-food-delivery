using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.BL.Facades.OrderFacade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Orders;

public class Review : PageModel
{
    [BindProperty]
    public RatingCreateDto NewRating { get; set; } = default!;
    
    public OrderWithProductsGetDto Order { get; set; } = default!;
    public bool SuccessfullyAddedRating { get; set; }

    private readonly IOrderFacade _orderFacade;

    public Review(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    public async Task<IActionResult> OnGet(Guid id)
    {
        var foundOrder = await _orderFacade.GetByIdAsync(id);
        if (foundOrder == null)
        {
            return NotFound();
        }

        Order = foundOrder;

        return Page();
    }
    
    public async Task<IActionResult> OnPost(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var foundOrder = await _orderFacade.GetByIdAsync(id);
        if (foundOrder == null)
        {
            return NotFound();
        }

        Order = foundOrder;
        
        NewRating.CreatedAt = DateTime.UtcNow;
        NewRating.OrderId = Order.Id;
        NewRating.RestaurantId = Order.Restaurant.Id;

        await _orderFacade.AddRatingForOrderAsync(NewRating);
        
        Order = await _orderFacade.GetByIdAsync(id);
        SuccessfullyAddedRating = true;
        return Page();
    }
}


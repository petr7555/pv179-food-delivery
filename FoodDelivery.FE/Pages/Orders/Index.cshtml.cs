using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Facades.OrderFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Orders;

[Authorize(Roles = "Customer")]
public class IndexModel : PageModel
{
    public IList<OrderWithProductsGetDto> Orders { get; set; } = default!;

    private readonly IOrderFacade _orderFacade;

    public IndexModel(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    public async Task OnGet()
    {
        Orders = (await _orderFacade.GetOrdersForUserAsync(User.Identity.Name)).ToList();
    }
}

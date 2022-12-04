using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "Customer")]
public class OrderList : PageModel
{
    public IEnumerable<OrderGetDto> Orders { get; set; }

    private readonly IOrderFacade _orderFacade;

    public OrderList(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    public async Task OnGet()
    {
        Orders = await _orderFacade.GetOrdersForUserAsync(User.Identity.Name);
    }
}

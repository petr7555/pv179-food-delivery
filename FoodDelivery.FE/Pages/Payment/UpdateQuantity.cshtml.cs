using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Services.OrderProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Payment;

public class UpdateQuantity : PageModel
{
    private readonly IOrderFacade _orderFacade;
    private readonly IOrderProductService _orderProductService;

    public UpdateQuantity(IOrderFacade orderFacade, IOrderProductService orderProductService)
    {
        _orderFacade = orderFacade;
        _orderProductService = orderProductService;
    }

    public async Task<IActionResult> OnPost(Guid productId, string returnUrl, [FromForm] int quantity)
    {
        var activeOrder = await _orderFacade.GetActiveOrderAsync(User.Identity.Name);
        if (activeOrder == null)
        {
            return NotFound();
        }
        
        await _orderFacade.UpdateQuantityAsync(activeOrder.Id, productId, quantity);
        
        return Redirect(returnUrl);
    }
}

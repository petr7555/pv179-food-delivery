using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Services.OrderProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Payment;

public class DeleteProduct : PageModel
{
    private readonly IOrderFacade _orderFacade;
    private readonly IOrderProductService _orderProductService;

    public DeleteProduct(IOrderFacade orderFacade, IOrderProductService orderProductService)
    {
        _orderFacade = orderFacade;
        _orderProductService = orderProductService;
    }

    public async Task<IActionResult> OnPost(Guid productId, string returnUrl)
    {
        var activeOrder = await _orderFacade.GetActiveOrderAsync(User.Identity.Name);
        if (activeOrder == null)
        {
            return NotFound();
        }

        await _orderFacade.DeleteProductFromOrderAsync(activeOrder, productId);

        return Redirect(returnUrl);
    }
}

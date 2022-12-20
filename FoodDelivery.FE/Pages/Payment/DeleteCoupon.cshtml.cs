using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Services.OrderProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Payment;

public class DeleteCoupon : PageModel
{
    private readonly IOrderFacade _orderFacade;
    private readonly IOrderProductService _orderProductService;

    public DeleteCoupon(IOrderFacade orderFacade, IOrderProductService orderProductService)
    {
        _orderFacade = orderFacade;
        _orderProductService = orderProductService;
    }

    public async Task<IActionResult> OnPost(Guid couponId, string returnUrl)
    {
        await _orderFacade.DeleteCouponFromOrderAsync(couponId);
        
        return Redirect(returnUrl);
    }
}

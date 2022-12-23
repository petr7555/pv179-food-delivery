using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Payment;

[Authorize(Roles = "Customer")]
public class Basket : PageModel
{
    public OrderWithProductsGetDto? Order { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "The coupon code must not be empty")]
    [Display(Name = "Coupon code:")]
    public string CouponCode { get; set; }

    private readonly IOrderFacade _orderFacade;
    private readonly IProductService _productService;

    public Basket(IOrderFacade orderFacade, IProductService productService)
    {
        _orderFacade = orderFacade;
        _productService = productService;
    }

    public async Task OnGet()
    {
        Order = await _orderFacade.GetActiveOrderAsync(User.Identity.Name);
    }

    public async Task<IActionResult> OnPost()
    {
        Order = await _orderFacade.GetActiveOrderAsync(User.Identity.Name);

        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        try
        {
            await _orderFacade.ApplyCouponCodeAsync(Order.Id, CouponCode);
        }
        catch (InvalidOperationException e)
        {
            ModelState.AddModelError(nameof(CouponCode), e.Message);
            return Page();
        }
        
        return RedirectToPage();
    }
}

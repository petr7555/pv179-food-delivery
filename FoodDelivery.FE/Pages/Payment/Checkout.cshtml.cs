using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.DAL.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Payment;

public class Checkout : PageModel
{
    public OrderWithProductsGetDto Order { get; set; }

    [BindProperty]
    [Display(Name = "Choose a payment method:")]
    public PaymentMethod PaymentMethod { get; set; }

    private readonly IOrderFacade _orderFacade;
    private readonly IProductService _productService;

    public Checkout(IOrderFacade orderFacade, IProductService productService)
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
        await _orderFacade.SetFinalCurrency(Order.Id, Order.CustomerDetails.Customer.UserSettings.SelectedCurrency.Id);

        if (Order?.TotalPrice.Amount == 0)
        {
            await _orderFacade.SetPaymentMethodAsync(Order.Id, PaymentMethod.Free);
            await _orderFacade.SubmitOrderAsync(Order.Id);
            await _orderFacade.MarkOrderAsPaid(Order.Id);
            return RedirectToPage("/Payment/Success");
        }

        switch (PaymentMethod)
        {
            case PaymentMethod.Cash:
                await _orderFacade.SetPaymentMethodAsync(Order.Id, PaymentMethod.Cash);
                await _orderFacade.SubmitOrderAsync(Order.Id);
                return RedirectToPage("/Payment/Success");
            case PaymentMethod.Card:
                var domain = Request.Scheme + "://" + Request.Host.Value;
                try
                {
                    var redirectUrl = await _orderFacade.PayByCardAsync(Order, domain);
                    Response.Headers.Add("Location", redirectUrl);
                    return new StatusCodeResult(303);
                }
                catch (InvalidOperationException e)
                {
                    ModelState.AddModelError("PaymentError", e.Message);
                    return Page();
                }
            case PaymentMethod.Free:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

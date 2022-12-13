using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace FoodDelivery.FE.Pages.Payment;

[Authorize(Roles = "Customer")]
public class Checkout : PageModel
{
    public List<ProductGetDto> ProductsInBasket { get; set; }

    private readonly IOrderFacade _orderFacade;
    private readonly IProductService _productService;

    public Checkout(IOrderFacade orderFacade, IProductService productService)
    {
        _orderFacade = orderFacade;
        _productService = productService;
    }

    public async Task OnGet()
    {
        var activeOrder = await _orderFacade.GetActiveOrderAsync(User.Identity.Name);
        ProductsInBasket = activeOrder?.Products ?? new List<ProductGetDto>();
    }

    /**
     * Use credit card number 4242 4242 4242 4242 for testing.
     */
    // TODO move logic to OrderService
    public async Task<IActionResult> OnPost()
    {
        var activeOrder = await _orderFacade.GetActiveOrderAsync(User.Identity.Name);
        var productsInBasket = activeOrder?.Products ??
                               throw new InvalidOperationException("Cannot checkout, no active order found.");

        var sessionLineItemOptions = new List<SessionLineItemOptions>();
        foreach (var p in productsInBasket)
        {
            var product = await _productService.GetByIdAsync(p.Id);
            sessionLineItemOptions.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(product.Price.Amount * 100),
                    Currency = product.Price.Currency.Name,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Name,
                    },
                },
                Quantity = 1,
            });
        }

        var domain = Request.Scheme + "://" + Request.Host.Value;
        var options = new SessionCreateOptions
        {
            LineItems = sessionLineItemOptions,
            Mode = "payment",
            SuccessUrl = domain + "/Payment/Success",
            CancelUrl = domain + "/Payment/Cancel",
            ClientReferenceId = activeOrder.Id.ToString(),
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}

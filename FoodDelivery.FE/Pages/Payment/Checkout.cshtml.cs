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
    public List<ProductLocalizedGetDto> ProductsInBasket { get; set; }

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
        ProductsInBasket = activeOrder?.Products ?? new List<ProductLocalizedGetDto>();
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

        var sessionLineItemOptions = productsInBasket.Select(p => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(p.Price.Amount * 100), Currency = p.Price.Currency.Name,
                ProductData = new SessionLineItemPriceDataProductDataOptions { Name = p.Name, },
            },
            Quantity = 1,
        }).ToList();

        var domain = Request.Scheme + "://" + Request.Host.Value;
        var options = new SessionCreateOptions
        {
            LineItems = sessionLineItemOptions,
            Mode = "payment",
            SuccessUrl = domain + "/Payment/Success",
            CancelUrl = domain + "/Payment/Cancel",
            ClientReferenceId = activeOrder.Id.ToString(),
            CustomerEmail = activeOrder.CustomerDetails.Customer.Email,
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}

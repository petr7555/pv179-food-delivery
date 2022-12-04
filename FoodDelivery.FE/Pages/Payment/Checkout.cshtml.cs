using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Services;
using FoodDelivery.BL.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace FoodDelivery.FE.Pages.Payment;

[Authorize(Roles = "Customer")]
public class Checkout : PageModel
{
    // TODO hardcode Order for now
    // [BindProperty]
    public OrderCreateDto Order { get; set; } = new()
    {
        Products = new List<OrderCreateDto.ProductDto>
        {
            new() { Id = 1, Quantity = 1 },
            new() { Id = 2, Quantity = 5 },
        },
    };

    private readonly IProductService _productService;

    public Checkout(IProductService productService)
    {
        _productService = productService;
    }

    public void OnGet()
    {
    }
    
    /**
     * Use credit card number 4242 4242 4242 4242 for testing.
     */
    // TODO move logic to OrderService
    public async Task<IActionResult> OnPost()
    {
        var sessionLineItemOptions = new List<SessionLineItemOptions>();
        foreach (var p in Order.Products)
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
                Quantity = p.Quantity,
            });
        }

        var domain = Request.Scheme + "://" + Request.Host.Value;
        var options = new SessionCreateOptions
        {
            LineItems = sessionLineItemOptions,
            Mode = "payment",
            SuccessUrl = domain + "/Payment/Success",
            CancelUrl = domain + "/Payment/Cancel",
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}

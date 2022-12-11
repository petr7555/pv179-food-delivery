using FoodDelivery.BL.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace FoodDelivery.FE.Controllers;

[Route("webhook/[controller]")]
public class StripeWebHook : ControllerBase
{
    // You can find your endpoint's secret in your webhook settings
    private const string Secret = "whsec_9fb831cc5126b1f3974f66f6c2534b8ee275e1f90cd029e87f9711680aa6e13c";

    private readonly IOrderService _orderService;

    public StripeWebHook(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        if (Request.Headers["Stripe-Signature"].Count == 0)
        {
            return BadRequest();
        }

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], Secret);

            // Handle the checkout.session.completed event
            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                var orderId = Guid.Parse(session.ClientReferenceId);

                // Fulfill the purchase...
                await _orderService.FulfillOrderAsync(orderId);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }
}

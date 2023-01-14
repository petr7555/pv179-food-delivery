using FoodDelivery.BL.Facades.OrderFacade;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using PaymentMethod = FoodDelivery.DAL.EntityFramework.Models.PaymentMethod;

namespace FoodDelivery.FE.Controllers;

[Route("webhook/[controller]")]
public class StripeWebhook : ControllerBase
{
    private readonly string _secret;

    private readonly IOrderFacade _orderFacade;

    public StripeWebhook(IOrderFacade orderFacade, IConfiguration configuration)
    {
        _orderFacade = orderFacade;
        _secret = configuration.GetSection("Stripe")["SecretWebhookKey"];
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
                Request.Headers["Stripe-Signature"], _secret);

            // Handle the checkout.session.completed event
            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                var orderId = Guid.Parse(session.ClientReferenceId);

                await _orderFacade.SetPaymentMethodAsync(orderId, PaymentMethod.Card);
                await _orderFacade.SubmitOrderAsync(orderId);
                await _orderFacade.MarkOrderAsPaid(orderId);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }
}

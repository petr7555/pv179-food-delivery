using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace FoodDelivery.FE.Pages.Payment;

public class Checkout : PageModel
{
    public void OnGet()
    {
        
    }
    
    public ActionResult OnPost()
    {
        var domain = Request.Scheme + "://" + Request.Host.Value;
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1M5mCXCexDij7OedL07zRGAW",
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = domain + "/Payment/Success",
            CancelUrl = domain + "/Payment/Cancel",
        };
        var service = new SessionService();
        var session = service.Create(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}

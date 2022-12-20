using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Facades.OrderFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Orders;

[Authorize(Roles = "Customer")]
public class IndexModel : PageModel
{
    public IList<OrderWithProductsGetDto> Orders { get; set; } = default!;

    private readonly IOrderFacade _orderFacade;

    public IndexModel(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    public async Task OnGet()
    {
        Orders = (await _orderFacade.GetOrdersForUserAsync(User.Identity.Name)).OrderByDescending(o => o.CreatedAt)
            .ToList();
    }

    public async Task<IActionResult> OnPost(Guid id)
    {
        var pdfPreviewUrl = $"https://{Request.Host}/Orders/PdfPreview?id={id}";
        var stream = await _orderFacade.CreatePdfFromOrder(pdfPreviewUrl);
        const string contentType = "application/pdf";
        var fileName = $"Order_{id}.pdf";
        return File(stream, contentType, fileName);
    }
}

using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.Facades.OrderFacade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Orders;

public class PdfPreviewModel : PageModel
{
    public OrderWithProductsGetDto Order { get; set; } = default!;

    private readonly IOrderFacade _orderFacade;

    public PdfPreviewModel(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    public async Task<IActionResult> OnGet(Guid id)
    {
        var foundOrder = await _orderFacade.GetByIdAsync(id);
        if (foundOrder == null)
        {
            return NotFound();
        }

        Order = foundOrder;

        return Page();
    }
}

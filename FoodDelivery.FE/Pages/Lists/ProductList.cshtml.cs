using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "Customer, ContentManager")]
public class ProductList : PageModel
{
    public IEnumerable<ProductGetDto> Products { get; set; }
    private readonly IProductFacade _productFacade;
    private readonly  IOrderFacade _orderFacade;

    public ProductList(IProductFacade productFacade, IOrderFacade orderFacade)
    {
        _productFacade = productFacade;
        _orderFacade = orderFacade;
    }

    public async Task OnGet()
    {
        Products = await _productFacade.GetAllAsync();
    }
    
    public async Task<IActionResult> OnPost(int id)
    {
        // TODO
        // await _orderFacade.AddToCartAsync(User.Identity.Name, id);
        
        return RedirectToPage("../Lists/ProductList");
    }
}

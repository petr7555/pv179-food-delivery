using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Facades.ProductFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "Customer, ContentManager")]
public class ProductList : PageModel
{
    public IEnumerable<ProductGetDto> Products { get; set; }

    private readonly IProductFacade _productFacade;
    private readonly IOrderFacade _orderFacade;

    public ProductList(IProductFacade productFacade, IOrderFacade orderFacade)
    {
        _productFacade = productFacade;
        _orderFacade = orderFacade;
    }

    public async Task OnGet(Guid restaurantId)
    {
        Products = await _productFacade.GetAllAsync();
        Products = Products.Where((product) => product.Restaurant.Id == restaurantId);    
    }

    public async Task<IActionResult> OnPost(Guid productId, Guid restaurantId)
    {
        await _orderFacade.AddProductToCartAsync(User.Identity.Name, productId);

        var redirectPage = RedirectToPage("../Lists/ProductList");

        redirectPage.RouteValues = new RouteValueDictionary();
        redirectPage.RouteValues.Add("restaurantId", restaurantId);

        return redirectPage;
    }
}

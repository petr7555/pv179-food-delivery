using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Facades.ProductFacade;
using FoodDelivery.BL.Facades.RestaurantFacade;
using FoodDelivery.BL.Services.PriceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "Customer, ContentManager")]
public class ProductList : PageModel
{
    public IEnumerable<ProductGetDto> Products { get; set; }
    public RestaurantGetDto Restaurant { get; set; }
    public PriceGetDto DeliveryPrice { get; set; }

    private readonly IProductFacade _productFacade;
    private readonly IOrderFacade _orderFacade;
    private readonly IRestaurantFacade _restaurantFacade;
    private readonly IPriceService _priceService;

    public ProductList(IProductFacade productFacade, IOrderFacade orderFacade, IRestaurantFacade restaurantFacade, IPriceService priceService)
    {
        _productFacade = productFacade;
        _orderFacade = orderFacade;
        _restaurantFacade = restaurantFacade;
        _priceService = priceService;
    }

    public async Task OnGet(Guid restaurantId)
    {
        Products = await _productFacade.GetAllAsync();
        Products = Products.Where((product) => product.Restaurant.Id == restaurantId);
        Restaurant = await _restaurantFacade.GetById(restaurantId);
        DeliveryPrice = await _priceService.GetByIdAsync(Restaurant.DeliveryPriceId);
    }

    public async Task<IActionResult> OnPost(Guid productId, Guid restaurantId)
    {
        await _orderFacade.AddProductToCartAsync(User.Identity.Name, productId);

        var redirectPage = RedirectToPage("../Lists/ProductList");

        redirectPage.RouteValues = new RouteValueDictionary();
        redirectPage.RouteValues.Add("restaurantId", restaurantId);

        return redirectPage;
    }

    public async Task<IActionResult> OnPostDeleteProduct(Guid productId, Guid restaurantId)
    {
        _productFacade.Delete(productId);
        return RedirectToPage("/Lists/ProductList", new { restaurantId = restaurantId });
    }

    public async Task<IActionResult> OnPostDeleteRestaurant(Guid restaurantId)
    {
        await _restaurantFacade.Delete(restaurantId);
        return RedirectToPage("/Lists/RestaurantList");
    }
}

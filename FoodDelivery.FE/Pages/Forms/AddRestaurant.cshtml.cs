using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades.RestaurantFacade;
using FoodDelivery.BL.Services.PriceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDelivery.FE.Pages.Forms;

[Authorize(Roles = "ContentManager")]
public class AddRestaurant : PageModel
{
    [BindProperty]
    public RestaurantCreateDto Restaurant { get; set; }
    [BindProperty]
    public PriceCreateDto DeliveryPrice { get; set; }
    public IEnumerable<CurrencyGetDto> Currencies { get; set; }
    [BindProperty]
    public string SelectedTag { get; set; }
    public SelectList TagOptions { get; set; }
    private readonly IRestaurantFacade _restaurantFacade;
    private readonly IPriceService _priceService;

    public AddRestaurant(IRestaurantFacade restaurantFacade, IPriceService priceService)
    {
        _restaurantFacade = restaurantFacade;
        _priceService = priceService;

        DeliveryPrice = new PriceCreateDto();
    }

    public async Task OnGetAsync()
    {
        Currencies = await _priceService.GetAllCurrencies();
        TagOptions = new SelectList(Currencies.ToList(), nameof(CurrencyGetDto.Id), nameof(CurrencyGetDto.Name));
    }

    public async Task<IActionResult> OnPost()
    {
        DeliveryPrice.Id = Guid.NewGuid();

        if (SelectedTag != null)
        {
            DeliveryPrice.CurrencyId = new Guid(SelectedTag);
        }
        else
        {
            ModelState.AddModelError("RestaurantCreationFailed", "Select currency!");
            return Page();
        }

        await _restaurantFacade.CreateWithNewPrice(Restaurant, DeliveryPrice);

        return RedirectToPage("../Lists/RestaurantList");
    }
}

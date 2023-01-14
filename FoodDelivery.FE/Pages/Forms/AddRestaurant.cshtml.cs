using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Facades.RestaurantFacade;
using FoodDelivery.BL.Services.PriceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms;

[Authorize(Roles = "ContentManager")]
public class AddRestaurant : PageModel
{
    [BindProperty]
    public RestaurantCreateDto Restaurant { get; set; }
    [BindProperty]
    public List<PriceCreateDto> Prices { get; set; }

    private readonly IRestaurantFacade _restaurantFacade;
    private readonly IPriceService _priceService;

    public AddRestaurant(IRestaurantFacade restaurantFacade, IPriceService priceService)
    {
        _restaurantFacade = restaurantFacade;
        _priceService = priceService;
        Prices = new List<PriceCreateDto>();        
    }

    public async Task OnGetAsync(Guid restaurantId)
    {
        if (restaurantId != Guid.Empty)
        {
            Restaurant = _restaurantFacade.ConvertToCreateDto(await _restaurantFacade.GetByIdAsync(restaurantId));
            var prices = await _priceService.GetAllAsCreateDtoAsync();
            Prices = prices.Where(price => price.RestaurantId.Equals(Restaurant.Id)).ToList();
        } else
        {
            var currencies = await _priceService.GetAllCurrencies();
            foreach (var currency in currencies)
            {
                var price = new PriceCreateDto
                {
                    CurrencyId = currency.Id,
                    Currency = currency,
                };
                Prices.Add(price);
            }
        }        
    }

    public async Task<IActionResult> OnPost()
    {
        if (Restaurant.Id == Guid.Empty)
        {
            Restaurant.Id = Guid.NewGuid();
            foreach (var price in Prices)
            {
                price.Id = Guid.NewGuid();
                price.RestaurantId = Restaurant.Id;
            }
            await _restaurantFacade.CreateWithNewPrices(Restaurant, Prices);
        }
        else
        {            
            await _restaurantFacade.UpdateAsync(Restaurant, Prices);
        }


        return RedirectToPage("../Lists/RestaurantList");
    }

    public PriceCreateDto GetPrice(int index)
    {
        return Prices.ElementAt(index);
    }
}

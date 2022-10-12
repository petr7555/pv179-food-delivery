using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Query;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryFE.Pages.Lists;

public class RestaurantList : PageModel
{
    public IEnumerable<Restaurant> Restaurants { get; set; }

    public void OnGet()
    {
        Restaurants = new EfQuery<Restaurant>().Where(r => r.DeliveryPrice.Amount < 100).Execute();
    }
}

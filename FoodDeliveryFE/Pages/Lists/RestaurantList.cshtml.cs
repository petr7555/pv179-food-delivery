using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Query;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryFE.Pages.Lists;

public class RestaurantList : PageModel
{
    public IEnumerable<Restaurant> Restaurants { get; set; }

    public void OnGet()
    {
        using (var context = new FoodDeliveryDbContext())
        {
            Restaurants = new EfQuery<Restaurant>(context)
                .Where(r => r.DeliveryPrice.Amount < 100)
                .OrderBy(r => r.Name)
                .Execute();
        }
    }
}

using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryFE.Pages.Lists;

public class RestaurantList : PageModel
{
    public IEnumerable<Restaurant> Restaurants { get; set; }

    public async Task OnGet()
    {
        await using (var uow = new EfUnitOfWork())
        {
            Restaurants = uow.RestaurantQuery
                .Where(r => r.DeliveryPrice.Amount < 100)
                .OrderBy(r => r.Name)
                .Execute();
        }
    }
}

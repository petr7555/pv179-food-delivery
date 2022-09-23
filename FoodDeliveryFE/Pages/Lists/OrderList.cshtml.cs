using FoodDeliveryDAL.Data;
using FoodDeliveryDAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryFE.Pages.Lists;

public class OrderList : PageModel
{
    public List<Order> Orders { get; set; }

    public void OnGet()
    {
        using (var context = new FoodDeliveryDbContext())
        {
            Orders = context.Orders.ToList();
        }
    }
}

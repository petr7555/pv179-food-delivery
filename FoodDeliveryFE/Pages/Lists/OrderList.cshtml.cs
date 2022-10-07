using FoodDeliveryDAL.Models;
using FoodDeliveryDAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryFE.Pages.Lists;

public class OrderList : PageModel
{
    public IEnumerable<Order> Orders { get; set; }

    public async Task OnGet()
    {
        await using (var uow = new EfUnitOfWork())
        {
            Orders = await uow.OrderRepository.GetAllAsync();
        }
    }
}

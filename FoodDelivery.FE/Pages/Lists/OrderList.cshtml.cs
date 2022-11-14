using FoodDelivery.DAL.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

public class OrderList : PageModel
{
    public IEnumerable<Order> Orders { get; set; }

    public async Task OnGet()
    {
        Orders = new List<Order>();
        // await using (var uow = new EfUnitOfWork())
        // {
        // Orders = await uow.OrderRepository.GetAllAsync();
        // Orders = new EfQuery<Order>().Where(o => o.Id > -1).Execute();
        // }
    }
}

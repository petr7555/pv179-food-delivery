using FoodDelivery.BL.Facades;
using FoodDelivery.DAL.EntityFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

[Authorize(Roles = "User")]
public class OrderList : PageModel
{
    public IEnumerable<Order> Orders { get; set; }

    private readonly IOrderFacade _orderFacade;

    public OrderList(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

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

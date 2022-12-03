using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms;

[Authorize(Roles = "Admin")]
public class AddContentManager : PageModel
{
    public void OnGet()
    {
    }

    public void OnPost()
    {
    }
}

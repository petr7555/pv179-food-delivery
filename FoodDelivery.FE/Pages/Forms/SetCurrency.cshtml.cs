using FoodDelivery.BL.Facades.UserFacade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms;

public class SetCurrency : PageModel
{
    private readonly IUserFacade _userFacade;

    public SetCurrency(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    public async Task<IActionResult> OnPost(Guid currencyId, string returnUrl)
    {
        await _userFacade.SetCurrencyAsync(User.Identity?.Name, currencyId);

        return Redirect(returnUrl);
    }
}

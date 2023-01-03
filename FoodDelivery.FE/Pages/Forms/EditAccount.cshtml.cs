using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.Facades.UserFacade;
using FoodDelivery.BL.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms;

public class EditAccount : PageModel
{
    [BindProperty]
    public CustomerDetailsUpdateDto CustomerDetails { get; set; }
    [BindProperty]
    public AddressUpdateDto BillingAddress { get; set; }
    [BindProperty]
    public AddressUpdateDto DeliveryAddress { get; set; }


    private readonly IUserFacade _userFacade;
    private readonly IUserService _userService;

    public EditAccount(IUserFacade userFacade, IUserService userService)
    {
        _userFacade = userFacade;
        _userService = userService;
    }

    public async Task OnGet()
    {
        var user = await _userService.GetByUsernameAsync(User.Identity.Name);
        CustomerDetails = _userService.ConvertToUpdateDto(user.CustomerDetails);
    }

    public async Task<IActionResult> OnPost(string email)
    {
        var user = await _userService.GetByUsernameAsync(User.Identity.Name);

        await _userFacade.UpdateAddressAsync(user.Id, user.CustomerDetails.BillingAddressId, CustomerDetails.BillingAddress);

        if (user.CustomerDetails.DeliveryAddressId.HasValue)
        {
            await _userFacade.UpdateAddressAsync(user.Id, user.CustomerDetails.DeliveryAddressId.Value, CustomerDetails.DeliveryAddress);
        }

        var userNew = await _userService.GetByUsernameAsync(User.Identity.Name);
        return null;
    }
}

using System.Security.Claims;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.Facades.UserFacade;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms;

public class EditAccount : PageModel
{
    // private readonly SignInManager<IdentityUser> _signInManager;
    // private readonly UserManager<IdentityUser> _userManager;
    // private readonly IUserFacade _userFacade;
    //
    // public EditAccount(IUserFacade userFacade, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    // {
    //     _signInManager = signInManager;
    //     _userManager = userManager;
    //     _userFacade = userFacade;
    // }
    
    // public async Task<IActionResult> OnPost(string email)
    // {
    //     var id = _userManager.FindByNameAsync(User.Identity?.Name).Id;
    //     var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
    //     var principal = System.Threading.Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;
    //     var userId = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
    //
    //     
    //     var dto = new CustomerDetailsUpdateDto();
    //     dto.Email = email;
    //     _userFacade.UpdateCustomerDetailsAsync(, dto);
    // }
}

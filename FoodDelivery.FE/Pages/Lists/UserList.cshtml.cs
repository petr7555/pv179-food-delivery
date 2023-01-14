using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Facades.UserFacade;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Lists;

public class UserList : PageModel
{
    public IEnumerable<UserGetDto> Users { get; set; }

    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserFacade _userFacade;

    public UserList(UserManager<IdentityUser> userManager, IUserFacade userFacade)
    {
        _userManager = userManager;
        _userFacade = userFacade;
        Users = _userFacade.GetAllAsync().Result;
    }

    public async void OnGet()
    {
    }

    public async Task<IActionResult> OnPostBanUser(Guid userId)
    {
        await _userFacade.BanUserAsync(userId);
        return RedirectToPage("/Lists/UserList");
    }

    public async Task<IActionResult> OnPostUnbanUser(Guid userId)
    {
        await _userFacade.UnbanUserAsync(userId);
        return RedirectToPage("/Lists/UserList");
    }

    public async Task<bool> IsBanned(Guid userId)
    {
        return await _userFacade.IsBanned(userId);
    }
}

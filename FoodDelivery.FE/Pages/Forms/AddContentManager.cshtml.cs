using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.DTOs.UserSettings;
using FoodDelivery.BL.Facades.UserFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Forms;

[Authorize(Roles = "Admin")]
public class AddContentManager : PageModel
{
    [BindProperty]
    [Required]
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }

    [BindProperty]
    [Required]
    [StringLength(100, MinimumLength = 4)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUserFacade _userFacade;

    public AddContentManager(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        IUserFacade userFacade)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userFacade = userFacade;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var newUser = new IdentityUser
        {
            UserName = Email,
            Email = Email,
        };

        var createResult = await _userManager.CreateAsync(newUser, Password);
        if (createResult.Succeeded)
        {
            var currency = await _userFacade.GetDefaultCurrencyAsync();
            await _userFacade.CreateUserAsync(new UserCreateDto
            {
                Email = Email,
                UserSettings = new UserSettingsCreateDto
                {
                    SelectedCurrencyId = currency.Id,
                }
            });
            var user = await _userManager.FindByEmailAsync(Email);

            await _userManager.AddToRoleAsync(user, "ContentManager");

            await _signInManager.SignInAsync(user, true);

            return Redirect("/");
        }

        foreach (var error in createResult.Errors.Where(e => e.Code != "DuplicateUserName"))
        {
            ModelState.AddModelError("RegistrationFailed", error.Description);
        }

        return Page();
    }
}

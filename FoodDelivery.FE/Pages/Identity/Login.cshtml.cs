using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.Facades.UserFacade;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Identity;

public class Login : PageModel
{
    [BindProperty]
    [Required]
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    private readonly IUserFacade _userFacade;

    private readonly ILogger<Login> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public Login(IUserFacade userFacade, ILogger<Login> logger, UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userFacade = userFacade;
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult OnGet([FromQuery] string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return Redirect(returnUrl ?? "/");
        }

        return Page();
    }

    public async Task<IActionResult> OnPost([FromQuery] string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Email);
        if (user is null)
        {
            ModelState.AddModelError("LoginFailed", "Account with this email does not exist");
            return Page();
        }

        if (!await _userManager.CheckPasswordAsync(user, Password))
        {
            ModelState.AddModelError("LoginFailed", "Incorrect password");
            return Page();
        }

        var NonIdentityUser = await _userFacade.GetByUsernameAsync(user.UserName);
        if (NonIdentityUser.Banned)
        {
            ModelState.AddModelError("LoginFailed", "Account has been banned");
            return Page();
        }

        var singInResult = await _signInManager.PasswordSignInAsync(Email, Password, true, false);
        if (singInResult.Succeeded)
        {
            return Redirect(returnUrl ?? "/");
        }

        ModelState.AddModelError("LoginFailed", "Login failed");
        _logger.LogWarning("Error logging in user {Email}", Email);
        return Page();
    }
}

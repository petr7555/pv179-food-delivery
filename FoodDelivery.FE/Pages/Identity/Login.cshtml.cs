using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Identity;

[BindProperties]
public class Login : PageModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    public string ReturnUrl { get; set; } = "/";

    private readonly ILogger<Register> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public Login(ILogger<Register> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
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
            
        var user = await _userManager.FindByEmailAsync(Email);

        if (user is null || await _userManager.CheckPasswordAsync(user, Password) == false)
        {
            ModelState.AddModelError(string.Empty, "Invalid credentials");
            return Page();
        }

        await _signInManager.SignOutAsync();
        var loggedIn = await _signInManager.PasswordSignInAsync(Email, Password, true, false);

        if (loggedIn.Succeeded)
        {
            return Redirect(ReturnUrl);
        }

        // This is not really the case, but we do not want the user to know the specifics of the error
        ModelState.AddModelError(string.Empty, "Invalid credentials");
        _logger.LogWarning("Error logging in user {Email}", Email);
        
        return Page();
    }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Identity;

[BindProperties]
public class Register : PageModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 4)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    private readonly ILogger<Register> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public Register(ILogger<Register> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
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
            
        var newUser = new IdentityUser
        {
            UserName = Email,
            Email = Email,
        };

        var createResult = await _userManager.CreateAsync(newUser, Password);
        if (createResult.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            _logger.LogInformation("Successfully created a new user account: {Email}", Email);

            await _userManager.AddToRoleAsync(user, "User");

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

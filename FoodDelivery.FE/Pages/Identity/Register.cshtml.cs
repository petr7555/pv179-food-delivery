using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Facades.UserFacade;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FE.Pages.Identity;

public class Register : PageModel
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

    private readonly ILogger<Register> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUserFacade _userFacade;


    public Register(ILogger<Register> logger, UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager, IUserFacade userFacade)
    {
        _logger = logger;
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
                CustomerDetails = new CustomerDetailsCreateDto
                {
                    SelectedCurrencyId = currency.Id,
                },
            });
            var user = await _userManager.FindByEmailAsync(Email);
            _logger.LogInformation("Successfully created a new customer account: {Email}", Email);

            await _userManager.AddToRoleAsync(user, "Customer");

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

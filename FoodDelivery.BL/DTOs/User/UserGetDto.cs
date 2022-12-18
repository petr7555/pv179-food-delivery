using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.UserSettings;

namespace FoodDelivery.BL.DTOs.User;

public class UserGetDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public Guid? CustomerDetailsId { get; set; }
    public CustomerDetailsGetDto? CustomerDetails { get; set; }
    public UserSettingsGetDto UserSettings { get; set; }
}

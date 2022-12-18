using FoodDelivery.BL.DTOs.UserSettings;

namespace FoodDelivery.BL.DTOs.User;

public class UserCreateDto
{
    public string Email { get; set; }
    public UserSettingsCreateDto UserSettings { get; set; }
}

using FoodDelivery.BL.DTOs.CustomerDetails;

namespace FoodDelivery.BL.DTOs.User;

public class UserCreateDto
{
    public string Email { get; set; }
    public CustomerDetailsCreateDto? CustomerDetails { get; set; }
}

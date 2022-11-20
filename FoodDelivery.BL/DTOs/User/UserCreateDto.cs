namespace FoodDelivery.BL.DTOs.User;

public class UserCreateDto
{
    public string Username { get; set; }

    public int RoleId { get; set; }

    public string PasswordHash { get; set; }
    public string Salt { get; set; }
}

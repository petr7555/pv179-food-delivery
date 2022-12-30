namespace FoodDelivery.BL.DTOs.User;

public class UserUpdateDto
{
    public Guid Id { get; set; }
    public bool Banned { get; set; }

    public Guid? CustomerDetailsId { get; set; }

    public string Username { get; set; }
}

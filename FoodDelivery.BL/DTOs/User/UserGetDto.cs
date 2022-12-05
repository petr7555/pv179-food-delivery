namespace FoodDelivery.BL.DTOs.User;

public class UserGetDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public Guid? CustomerDetailsId { get; set; }
}

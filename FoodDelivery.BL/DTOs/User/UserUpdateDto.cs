using FoodDelivery.BL.DTOs.CustomerDetails;

namespace FoodDelivery.BL.DTOs.User;

public class UserUpdateDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool Banned { get; set; }
    public Guid CustomerDetailsId { get; set; }
    public CustomerDetailsUpdateDto CustomerDetails { get; set; }
}

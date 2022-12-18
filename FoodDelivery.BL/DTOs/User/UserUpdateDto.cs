using FoodDelivery.BL.DTOs.CustomerDetails;

namespace FoodDelivery.BL.DTOs.User;

public class UserUpdateDto
{
    public Guid CustomerDetailsId { get; set; }
    public CustomerDetailsUpdateDto CustomerDetails { get; set; }
}

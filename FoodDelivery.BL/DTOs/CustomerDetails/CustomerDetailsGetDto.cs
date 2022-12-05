using FoodDelivery.BL.DTOs.User;

namespace FoodDelivery.BL.DTOs.CustomerDetails;

public class CustomerDetailsGetDto
{
    public Guid Id { get; set; }
    public UserGetDto Customer  { get; set; }
}

using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.User;

namespace FoodDelivery.BL.DTOs.CustomerDetails;

public class CustomerDetailsGetDto
{
    public Guid Id { get; set; }
    public UserGetDto Customer { get; set; }
    public Guid BillingAddressId { get; set; }

    public AddressGetDto BillingAddress { get; set; }

    public Guid? DeliveryAddressId { get; set; }

    public AddressGetDto DeliveryAddress { get; set; }
}

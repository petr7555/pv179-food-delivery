using FoodDelivery.BL.DTOs.Address;

namespace FoodDelivery.BL.DTOs.CustomerDetails;

public class CustomerDetailsUpdateDto
{
    public Guid Id { get; set; }
    public Guid BillingAddressId { get; set; }
    public AddressUpdateDto BillingAddress { get; set; }
    public Guid? DeliveryAddressId { get; set; }
    public AddressUpdateDto? DeliveryAddress { get; set; }
}

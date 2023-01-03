using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CompanyInfo;
using FoodDelivery.BL.DTOs.User;

namespace FoodDelivery.BL.DTOs.CustomerDetails;

public class CustomerDetailsGetDto
{
    public Guid Id { get; set; }
    public UserGetDto Customer { get; set; }
    public Guid BillingAddressId { get; set; }
    public virtual AddressGetDto BillingAddress { get; set; }
    public Guid? DeliveryAddressId { get; set; }
    public virtual AddressGetDto? DeliveryAddress { get; set; }
    public virtual CompanyInfoGetDto? CompanyInfo { get; set; }
}

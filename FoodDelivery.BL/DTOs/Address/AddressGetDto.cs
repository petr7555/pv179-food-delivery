using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Address;

public class AddressGetDto
{
    [Display(Name = "Full name")]
    public string FullName { get; set; }

    [Display(Name = "Street address")]
    public string StreetAddress { get; set; }

    public string City { get; set; }
    public string State { get; set; }

    [Display(Name = "Zip code")]
    public string ZipCode { get; set; }

    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }
}

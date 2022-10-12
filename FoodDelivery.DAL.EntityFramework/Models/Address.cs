using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Address : BaseEntity
{
    [MaxLength(255)]
    public string FullName { get; set; }

    [MaxLength(255)]
    public string StreetAddress { get; set; }

    [MaxLength(255)]
    public string City { get; set; }

    [MaxLength(255)]
    public string State { get; set; }

    [MaxLength(255)]
    public string ZipCode { get; set; }

    [MaxLength(255)]
    public string PhoneNumber { get; set; }
}

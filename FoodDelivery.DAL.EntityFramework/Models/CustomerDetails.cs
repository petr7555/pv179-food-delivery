using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class CustomerDetails : BaseEntity
{
    public User Customer  { get; set; }
    
    [MaxLength(255)]
    public string Email { get; set; }

    public int BillingAddressId { get; set; }

    [ForeignKey(nameof(BillingAddressId))]
    public virtual Address BillingAddress { get; set; }

    public int? DeliveryAddressId { get; set; }

    [ForeignKey(nameof(DeliveryAddressId))]
    public virtual Address? DeliveryAddress { get; set; }

    public int? CompanyInfoId { get; set; }

    [ForeignKey(nameof(CompanyInfoId))]
    public virtual CompanyInfo? CompanyInfo { get; set; }
}

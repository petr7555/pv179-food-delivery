using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class CustomerDetails : BaseEntity
{
    public User Customer { get; set; }

    public Guid BillingAddressId { get; set; }

    [ForeignKey(nameof(BillingAddressId))]
    public virtual Address BillingAddress { get; set; }

    public Guid? DeliveryAddressId { get; set; }

    [ForeignKey(nameof(DeliveryAddressId))]
    public virtual Address? DeliveryAddress { get; set; }

    public Guid? CompanyInfoId { get; set; }

    [ForeignKey(nameof(CompanyInfoId))]
    public virtual CompanyInfo? CompanyInfo { get; set; }
}

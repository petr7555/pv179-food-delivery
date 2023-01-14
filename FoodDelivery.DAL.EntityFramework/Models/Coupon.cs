using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Models;

[Index(nameof(Code), IsUnique = true)]
public class Coupon : BaseEntity
{
    [MaxLength(255)]
    public string Code { get; set; }

    public DateTime ValidUntil { get; set; }

    public CouponStatus Status { get; set; }

    public virtual List<Price> Prices { get; set; }

    public Guid? OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public virtual Order? Order { get; set; }
    
    public Guid? FinalPriceId { get; set; }

    [ForeignKey(nameof(FinalPriceId))]
    public virtual Price? FinalPrice { get; set; }
}

public enum CouponStatus
{
    Valid,
    Revoked,
    Used,
}

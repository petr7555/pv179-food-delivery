using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class PaymentMethod : BaseEntity
{
    [MaxLength(255)]
    public string Type { get; set; }

    public int? CouponId { get; set; }

    [ForeignKey(nameof(CouponId))]
    public virtual Coupon? Coupon { get; set; }
}

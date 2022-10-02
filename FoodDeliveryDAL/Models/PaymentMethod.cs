using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class PaymentMethod : BaseEntity
{
    public string Type { get; set; }
    public int? CouponId { get; set; }
    [ForeignKey(nameof(CouponId))] public virtual Coupon? Coupon { get; set; }
}

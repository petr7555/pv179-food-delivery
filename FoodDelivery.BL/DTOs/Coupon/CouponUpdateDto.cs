using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Coupon;

public class CouponUpdateDto
{
    public Guid Id { get; set; }
    public CouponStatus Status { get; set; }
    public Guid? OrderId { get; set; }
}

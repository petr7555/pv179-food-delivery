using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Coupon;

public class CouponLocalizedGetDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public DateTime ValidUntil { get; set; }
    public CouponStatus Status { get; set; }
    public PriceGetDto Price { get; set; }
}

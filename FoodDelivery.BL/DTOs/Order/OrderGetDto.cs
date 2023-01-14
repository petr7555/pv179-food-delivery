using FoodDelivery.BL.DTOs.Coupon;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderGetDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CustomerDetailsId { get; set; }
    public CustomerDetailsGetDto CustomerDetails { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderProductGetDto> OrderProducts { get; set; }
    public List<CouponGetDto> Coupons { get; set; }
    public RatingGetDto? Rating { get; set; }
    public CurrencyGetDto? FinalCurrency { get; set; }
    public PriceGetDto? FinalDeliveryPrice { get; set; }
}

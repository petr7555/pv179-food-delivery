using FoodDelivery.BL.DTOs.Coupon;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderGetDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CustomerDetailsId { get; set; }
    public CustomerDetailsGetDto CustomerDetails { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderProductGetDto> OrderProducts { get; set; }
    public List<CouponGetDto> Coupons { get; set; }
}

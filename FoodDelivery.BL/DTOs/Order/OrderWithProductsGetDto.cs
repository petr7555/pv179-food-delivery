using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Coupon;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderWithProductsGetDto
{
    [Display(Name = "Order number")]
    public Guid Id { get; set; }

    [Display(Name = "Created at")]
    public DateTime CreatedAt { get; set; }

    public CustomerDetailsGetDto CustomerDetails { get; set; }

    [Display(Name = "Order status")]
    public OrderStatus Status { get; set; }

    public List<ProductLocalizedGetDto> Products { get; set; }

    [Display(Name = "Total price")]
    public PriceGetDto TotalPrice { get; set; }
    
    public List<CouponLocalizedGetDto> Coupons { get; set; }
}

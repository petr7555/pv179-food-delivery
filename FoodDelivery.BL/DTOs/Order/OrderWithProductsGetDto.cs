using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.CustomerDetails;
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
    public List<ProductGetDto> Products { get; set; }
}

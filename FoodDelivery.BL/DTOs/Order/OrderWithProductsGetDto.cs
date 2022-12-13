using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderWithProductsGetDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public CustomerDetailsGetDto CustomerDetails { get; set; }
    public OrderStatus Status { get; set; }
    public List<ProductGetDto> Products { get; set; }
}

using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderGetDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public CustomerDetailsGetDto CustomerDetails { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderProductGetDto> OrderProducts { get; set; }
}

using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderUpdateDto
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
}

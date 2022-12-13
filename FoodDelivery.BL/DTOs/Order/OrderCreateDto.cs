using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderCreateDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CustomerDetailsId { get; set; }
    public virtual OrderStatus Status { get; set; }
}

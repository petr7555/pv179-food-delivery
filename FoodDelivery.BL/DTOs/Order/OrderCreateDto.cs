using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderCreateDto
{
    // TODO
    // public class ProductDto
    // {
    // public int Id { get; set; }
    // public int Quantity { get; set; }
    // }

    public Guid Id { get; set; }
    public Guid CustomerDetailsId { get; set; }
    public virtual OrderStatus OrderStatus { get; set; }
}

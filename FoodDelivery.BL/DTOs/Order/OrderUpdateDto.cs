using FoodDelivery.BL.DTOs.OrderProduct;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderUpdateDto
{
    public Guid Id { get; set; }
    public List<OrderProductCreateDto> OrderProducts { get; set; }
}

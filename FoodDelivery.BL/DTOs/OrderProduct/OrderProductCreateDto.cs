namespace FoodDelivery.BL.DTOs.OrderProduct;

public class OrderProductCreateDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

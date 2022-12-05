namespace FoodDelivery.BL.DTOs.OrderProduct;

public class OrderProductGetDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
}

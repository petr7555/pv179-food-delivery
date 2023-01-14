namespace FoodDelivery.BL.DTOs.OrderProduct;

public class OrderProductUpdateDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid FinalPriceId { get; set; }
}

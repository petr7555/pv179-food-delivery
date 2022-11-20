namespace FoodDelivery.BL.DTOs.Order;

public class OrderCreateDto
{
    public int CustomerId { get; set; }
    public int PaymentMethodId { get; set; }
    public class ProductDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public List<ProductDto> Products { get; set; }
}

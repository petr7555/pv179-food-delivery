namespace FoodDelivery.BL.DTOs.Order;

public class OrderCreateDto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public List<ProductDto> Products { get; set; }
}

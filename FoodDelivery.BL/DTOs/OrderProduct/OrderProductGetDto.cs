using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;

namespace FoodDelivery.BL.DTOs.OrderProduct;

public class OrderProductGetDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public ProductGetDto Product { get; set; }
    public int Quantity { get; set; }
    public PriceGetDto FinalPrice { get; set; }
}

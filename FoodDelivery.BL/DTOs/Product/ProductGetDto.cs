using FoodDelivery.BL.DTOs.Price;

namespace FoodDelivery.BL.DTOs.Product;

public class ProductGetDto
{
    public string Name { get; set; }
    public PriceGetDto Price { get; set; }
}

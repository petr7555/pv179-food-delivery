using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;

namespace FoodDelivery.BL.DTOs.Product;

public class ProductGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryGetDto Category { get; set; }
    public RestaurantGetDto Restaurant { get; set; }
    public PriceGetDto Price { get; set; }
}

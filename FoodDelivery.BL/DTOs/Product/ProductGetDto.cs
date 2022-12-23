using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;

namespace FoodDelivery.BL.DTOs.Product;

public class ProductGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    [Display(Name = "Image")]
    public string ImageUrl { get; set; }

    public CategoryGetDto Category { get; set; }
    public RestaurantGetDto Restaurant { get; set; }
    public List<PriceGetDto> Prices { get; set; }
    public int Quantity { get; set; }
}

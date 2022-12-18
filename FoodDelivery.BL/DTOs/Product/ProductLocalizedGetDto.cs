using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;

namespace FoodDelivery.BL.DTOs.Product;

public class ProductLocalizedGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [Display(Name = "Image")]
    public string ImageUrl { get; set; }
    public CategoryGetDto Category { get; set; }
    public RestaurantGetDto Restaurant { get; set; }
    public PriceGetDto Price { get; set; }
}

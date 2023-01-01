using FoodDelivery.BL.DTOs.Price;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Product;

public class ProductCreateDto
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string Description { get; set; }

    [Display(Name = "Image")]
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Guid PriceId { get; set; }
    public PriceCreateDto Price { get; set; }
}

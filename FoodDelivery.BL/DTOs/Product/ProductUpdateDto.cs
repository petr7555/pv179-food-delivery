using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Price;

namespace FoodDelivery.BL.DTOs.Product;

public class ProductUpdateDto
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string Description { get; set; }

    [Display(Name = "Image")]
    public string ImageUrl { get; set; }

    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public List<PriceUpdateDto> Prices { get; set; }
}

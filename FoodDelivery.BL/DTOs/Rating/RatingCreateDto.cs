using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Rating;

public class RatingCreateDto
{
    public Guid RestaurantId { get; set; }

    public Guid OrderId { get; set; }

    public DateTime CreatedAt { get; set; }

    [Range(1, 5)]
    public int Stars { get; set; }

    public string? Comment { get; set; }
}

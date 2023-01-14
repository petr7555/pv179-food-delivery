namespace FoodDelivery.BL.DTOs.Rating;

public class RatingGetDto
{
    public Guid RestaurantId { get; set; }

    public int Stars { get; set; }

    public string? Comment { get; set; }
}

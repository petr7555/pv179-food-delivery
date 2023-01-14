using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Rating;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantLocalizedGetDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    [Display(Name = "Delivery price")]
    public PriceGetDto DeliveryPrice { get; set; }

    [Display(Name = "Average rating")]
    public double? AverageRating { get; set; }

    public List<RatingGetDto> Ratings { get; set; }
}

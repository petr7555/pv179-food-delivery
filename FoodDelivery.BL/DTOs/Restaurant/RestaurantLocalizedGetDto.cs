using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Price;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantLocalizedGetDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    [Display(Name = "Delivery price")]
    public PriceGetDto DeliveryPrice { get; set; }
}

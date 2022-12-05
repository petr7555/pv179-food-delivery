using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantCreateDto
{
    [StringLength(255)]
    public string Name { get; set; }

    public Guid DeliveryPriceId { get; set; }
}

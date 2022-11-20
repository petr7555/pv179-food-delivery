using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantCreateDto
{
    [StringLength(255)]
    public string Name { get; set; }

    public int DeliveryPriceId { get; set; }
}

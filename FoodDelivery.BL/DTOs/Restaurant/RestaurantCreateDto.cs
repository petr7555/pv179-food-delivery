using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantCreateDto
{
    public Guid Id { get; set; }
    [StringLength(255)]
    public string Name { get; set; }

    public Guid DeliveryPriceId { get; set; }
}

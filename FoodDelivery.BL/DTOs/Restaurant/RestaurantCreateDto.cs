using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantCreateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DeliveryPriceId { get; set; }
}

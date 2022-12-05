using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantUpdateDto
{
    public Guid Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    public Guid DeliveryPriceId { get; set; }

    public float DeliveryPriceAmount { get; set; }

    public Guid CurrencyId { get; set; }
}

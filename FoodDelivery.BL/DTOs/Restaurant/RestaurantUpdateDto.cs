using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantUpdateDto
{
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    public int DeliveryPriceId { get; set; }

    public float DeliveryPriceAmount { get; set; }

    public int CurrencyId { get; set; }
}

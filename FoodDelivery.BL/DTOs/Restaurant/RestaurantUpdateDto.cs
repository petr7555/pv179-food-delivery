using System.ComponentModel.DataAnnotations;
using FoodDelivery.BL.DTOs.Price;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantUpdateDto
{
    public Guid Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    public List<PriceUpdateDto> DeliveryPrices { get; set; }
}

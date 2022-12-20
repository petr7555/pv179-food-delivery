using FoodDelivery.BL.DTOs.Price;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<PriceGetDto> DeliveryPrices { get; set; }
}

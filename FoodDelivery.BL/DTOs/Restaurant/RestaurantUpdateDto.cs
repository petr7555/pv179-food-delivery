using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DeliveryPriceId { get; set; }
    public Price DeliveryPrice { get; set; }
}

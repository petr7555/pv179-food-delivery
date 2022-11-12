using System.ComponentModel.DataAnnotations.Schema;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantCreateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DeliveryPriceId { get; set; }
    public Price DeliveryPrice { get; set; }
}

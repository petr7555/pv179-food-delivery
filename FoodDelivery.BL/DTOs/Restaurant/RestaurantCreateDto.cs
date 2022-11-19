using FoodDelivery.DAL.EntityFramework.Models;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantCreateDto
{
    public int Id { get; set; }
    [StringLength(255)]
    public string Name { get; set; }
    public int DeliveryPriceId { get; set; }
    public Price DeliveryPrice { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.Restaurant;

public class RestaurantUpdateDto
{
    public Guid Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }
}

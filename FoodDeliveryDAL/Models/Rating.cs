using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class Rating: BaseEntity
{
    public int RestaurantId { get; set; }
    [ForeignKey(nameof(RestaurantId))]
    public virtual Restaurant Restaurant { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stars { get; set; }
    public int DeliveryTimeMin { get; set; }
    public string Comment { get; set; }
}
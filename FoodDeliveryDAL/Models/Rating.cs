using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class Rating : BaseEntity
{
    public int RestaurantId { get; set; }

    [ForeignKey(nameof(RestaurantId))]
    public virtual Restaurant Restaurant { get; set; }

    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; }

    public DateTime CreatedAt { get; set; }

    [Range(1, 5)]
    public int Stars { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }
}

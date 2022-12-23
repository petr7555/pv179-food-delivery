using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Rating : BaseEntity
{
    public Guid RestaurantId { get; set; }

    [ForeignKey(nameof(RestaurantId))]
    public virtual Restaurant Restaurant { get; set; }

    public Guid OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Stars { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Product : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }

    public string? Description { get; set; }

    [MaxLength(255)]
    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }

    public int RestaurantId { get; set; }

    [ForeignKey(nameof(RestaurantId))]
    public virtual Restaurant Restaurant { get; set; }

    public int PriceId { get; set; }

    [ForeignKey(nameof(PriceId))]
    public virtual Price Price { get; set; }

    public virtual List<Order> Orders { get; set; }
}

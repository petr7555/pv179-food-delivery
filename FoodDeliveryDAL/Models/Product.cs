using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class Product : BaseEntity
{
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))] public virtual Category Category { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public int RestaurantId { get; set; }
    [ForeignKey(nameof(RestaurantId))] public virtual Restaurant Restaurant { get; set; }
    public int PriceId { get; set; }
    [ForeignKey(nameof(PriceId))] public virtual Price Price { get; set; }

    public virtual List<OrderProduct> OrderProducts { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Product : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }

    public string Description { get; set; }

    [MaxLength(255)]
    public string ImageUrl { get; set; }

    public Guid CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }

    public Guid RestaurantId { get; set; }

    [ForeignKey(nameof(RestaurantId))]
    public virtual Restaurant Restaurant { get; set; }

    public virtual List<Price> Prices { get; set; }

    public virtual List<OrderProduct> OrderProducts { get; set; }
}

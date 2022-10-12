using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Category : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }

    public int? ParentCategoryId { get; set; }

    [ForeignKey(nameof(ParentCategoryId))]
    public virtual Category? ParentCategory { get; set; }
}

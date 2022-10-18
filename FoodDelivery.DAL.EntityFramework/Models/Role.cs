using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Role : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }
}

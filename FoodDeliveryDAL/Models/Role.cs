using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryDAL.Models;

public class Role : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }
}

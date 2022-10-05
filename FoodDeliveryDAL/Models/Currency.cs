using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryDAL.Models;

public class Currency : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }
}

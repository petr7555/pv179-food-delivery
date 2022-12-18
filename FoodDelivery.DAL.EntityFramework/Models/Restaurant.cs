using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Restaurant : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }
    
    public virtual List<Price> DeliveryPrices { get; set; }
}

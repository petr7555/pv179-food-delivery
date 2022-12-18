using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Restaurant : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }

    public virtual List<Price> DeliveryPrices { get; set; }
}

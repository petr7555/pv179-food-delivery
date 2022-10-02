using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class Restaurant: BaseEntity
{
    public string Name { get; set; }
    public int DeliveryPriceID { get; set; }
    [ForeignKey(nameof(DeliveryPriceID))]
    public virtual Price DeliveryPrice { get; set; }
}
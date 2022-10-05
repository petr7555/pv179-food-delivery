using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class Restaurant : BaseEntity
{
    public string Name { get; set; }

    public int DeliveryPriceId { get; set; }

    [ForeignKey(nameof(DeliveryPriceId))]
    public virtual Price DeliveryPrice { get; set; }
}

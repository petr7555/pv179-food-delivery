using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class Price: BaseEntity
{
    public float Amount { get; set; }
    public int CurrencyId { get; set; }
    [ForeignKey(nameof(CurrencyId))]
    public virtual Currency Currency { get; set; }
}
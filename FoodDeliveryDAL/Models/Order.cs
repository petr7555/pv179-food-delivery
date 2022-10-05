using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models;

public class Order : BaseEntity
{
    public DateTime CreatedAt { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public virtual CustomerDetails Customer { get; set; }

    public int PaymentMethodId { get; set; }

    [ForeignKey(nameof(PaymentMethodId))]
    public virtual PaymentMethod PaymentMethod { get; set; }

    public virtual List<Product> Products { get; set; }
}

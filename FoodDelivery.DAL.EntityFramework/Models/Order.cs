using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Order : BaseEntity
{
    public DateTime CreatedAt { get; set; }

    public int CustomerDetailsId { get; set; }

    [ForeignKey(nameof(CustomerDetailsId))]
    public virtual CustomerDetails CustomerDetails { get; set; }

    public int PaymentMethodId { get; set; }

    [ForeignKey(nameof(PaymentMethodId))]
    public virtual PaymentMethod PaymentMethod { get; set; }

    public virtual List<Product> Products { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Order : BaseEntity
{
    public DateTime CreatedAt { get; set; }

    public Guid CustomerDetailsId { get; set; }

    [ForeignKey(nameof(CustomerDetailsId))]
    public virtual CustomerDetails CustomerDetails { get; set; }

    // TODO
    // public Guid PaymentMethodId { get; set; }

    // [ForeignKey(nameof(PaymentMethodId))]
    // public virtual PaymentMethod PaymentMethod { get; set; }

    public virtual OrderStatus OrderStatus { get; set; }

    public virtual List<OrderProduct> OrderProducts { get; set; }
}

public enum OrderStatus
{
    Active,
    Submitted,
    Paid,
}

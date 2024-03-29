using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class Order : BaseEntity
{
    public DateTime CreatedAt { get; set; }

    public Guid CustomerDetailsId { get; set; }

    [ForeignKey(nameof(CustomerDetailsId))]
    public virtual CustomerDetails CustomerDetails { get; set; }

    public Guid? FinalCurrencyId { get; set; }

    [ForeignKey(nameof(FinalCurrencyId))]
    public virtual Currency? FinalCurrency { get; set; }

    public Guid? FinalDeliveryPriceId { get; set; }

    [ForeignKey(nameof(FinalDeliveryPriceId))]
    public virtual Price? FinalDeliveryPrice { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }

    public OrderStatus Status { get; set; }

    public virtual List<OrderProduct> OrderProducts { get; set; }

    public virtual List<Coupon> Coupons { get; set; }

    public virtual Rating? Rating { get; set; }
}

public enum PaymentMethod
{
    Free,
    Cash,
    Card,
}

public enum OrderStatus
{
    Active,
    Submitted,
    Paid,
}

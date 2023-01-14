using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class OrderProduct : BaseEntity
{
    public Guid OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; }

    public Guid ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }

    public Guid? FinalPriceId { get; set; }

    [ForeignKey(nameof(FinalPriceId))]
    public Price? FinalPrice { get; set; }

    public int Quantity { get; set; }
}

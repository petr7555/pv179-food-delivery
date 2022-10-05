using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models
{
    public class OrderProduct : BaseEntity
    {
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}

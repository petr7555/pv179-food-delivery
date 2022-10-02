using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryDAL.Models
{
    public class CustomerDetails : BaseEntity
    {
        public string Email { get; set; }

        public int? BillingAddressId { get; set; }

        [ForeignKey(nameof(BillingAddressId))]
        public virtual Address BillingAddress { get; set; }

        public int DeliveryAddressId { get; set; }

        [ForeignKey(nameof(BillingAddressId))]
        public virtual Address DeliveryAddress { get; set; }

        public int CompanyInfoId { get; set; }

        [ForeignKey(nameof(CompanyInfoId))]
        public virtual CompanyInfo CompanyInfo { get; set; }
    }
}
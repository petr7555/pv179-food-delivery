using FoodDelivery.BL.DTOs.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.BL.DTOs.Price
{
    public class PriceUpdateDto
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? RestaurantId { get; set; }
        public float Amount { get; set; }
        public Guid CurrencyId { get; set; }
        public CurrencyGetDto Currency { get; set; }
    }
}
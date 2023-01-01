using FoodDelivery.BL.DTOs.Currency;

namespace FoodDelivery.BL.DTOs.Price;
public class PriceCreateDto
{
    public Guid Id { get; set; }
    public float Amount { get; set; }
    public Guid CurrencyId { get; set; }
}


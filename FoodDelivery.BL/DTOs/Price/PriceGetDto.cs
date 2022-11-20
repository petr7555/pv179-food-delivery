using FoodDelivery.BL.DTOs.Currency;

namespace FoodDelivery.BL.DTOs.Price;

public class PriceGetDto
{
    public float Amount { get; set; }
    public CurrencyGetDto Currency { get; set; }
}

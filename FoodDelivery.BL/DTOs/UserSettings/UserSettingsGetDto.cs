using FoodDelivery.BL.DTOs.Currency;

namespace FoodDelivery.BL.DTOs.UserSettings;

public class UserSettingsGetDto
{
    public Guid Id { get; set; }
    public CurrencyGetDto SelectedCurrency { get; set; }
}

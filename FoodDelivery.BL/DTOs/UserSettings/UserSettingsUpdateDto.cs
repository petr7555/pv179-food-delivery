namespace FoodDelivery.BL.DTOs.UserSettings;

public class UserSettingsUpdateDto
{
    public Guid Id { get; set; }
    public Guid SelectedCurrencyId { get; set; }
}

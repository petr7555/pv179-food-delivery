using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class UserSettings : BaseEntity
{
    public Guid SelectedCurrencyId { get; set; }

    [ForeignKey(nameof(SelectedCurrencyId))]
    public virtual Currency SelectedCurrency { get; set; }
}

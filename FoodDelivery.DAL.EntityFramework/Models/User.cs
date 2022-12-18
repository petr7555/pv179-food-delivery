using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Models;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity
{
    [MaxLength(255)]
    public string Email { get; set; }

    public bool Banned { get; set; }

    public Guid? CustomerDetailsId { get; set; }

    [ForeignKey(nameof(CustomerDetailsId))]
    public virtual CustomerDetails? CustomerDetails { get; set; }

    public Guid UserSettingsId { get; set; }

    [ForeignKey(nameof(UserSettingsId))]
    public virtual UserSettings UserSettings { get; set; }
}

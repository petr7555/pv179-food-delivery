using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Models;

[Index(nameof(Username), IsUnique = true)]
public class User : BaseEntity
{
    [MaxLength(255)]
    public string Username { get; set; }

    public bool Banned { get; set; }

    public int? CustomerDetailsId { get; set; }

    [ForeignKey(nameof(CustomerDetailsId))]
    public virtual CustomerDetails? CustomerDetails { get; set; }
}

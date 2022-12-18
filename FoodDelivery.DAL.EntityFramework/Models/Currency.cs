using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Models;

[Index(nameof(Name), IsUnique = true)]
public class Currency : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; }
}

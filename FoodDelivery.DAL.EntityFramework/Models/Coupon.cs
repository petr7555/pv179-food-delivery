using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Models;

[Index(nameof(Code), IsUnique = true)]
public class Coupon : BaseEntity
{
    [MaxLength(255)]
    public string Code { get; set; }

    public DateTime ValidUntil { get; set; }
    public bool Valid { get; set; }

    public Guid AmountId { get; set; }

    [ForeignKey(nameof(AmountId))]
    public virtual Price Amount { get; set; }
}

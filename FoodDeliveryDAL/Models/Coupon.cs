using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryDAL.Models;

[Index(nameof(Code), IsUnique = true)]
public class Coupon : BaseEntity
{
    public string Code { get; set; }
    public DateTime ValidUntil { get; set; }
    public bool Valid { get; set; }

    public int AmountId { get; set; }

    [ForeignKey(nameof(AmountId))]
    public virtual Price Amount { get; set; }
}

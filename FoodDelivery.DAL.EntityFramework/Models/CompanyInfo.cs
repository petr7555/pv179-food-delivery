using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DAL.EntityFramework.Models;

public class CompanyInfo : BaseEntity
{
    [MaxLength(255)]
    public string Vat { get; set; }

    [MaxLength(255)]
    public string CompanyName { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.BL.DTOs.CompanyInfo;

public class CompanyInfoGetDto
{
    public Guid Id { get; set; }
    public string Vat { get; set; }

    [Display(Name = "Company name")]
    public string CompanyName { get; set; }
}

namespace FoodDeliveryDAL.Models;

public class Coupon: BaseEntity
{
    public string Code { get; set; }
    public DateTime ValidUntil { get; set; }
    public bool Valid { get; set; }
}
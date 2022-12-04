using FoodDelivery.BL.DTOs.CustomerDetails;

namespace FoodDelivery.BL.DTOs.Order;

public class OrderGetDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual CustomerDetailsGetDto CustomerDetails { get; set; }
}

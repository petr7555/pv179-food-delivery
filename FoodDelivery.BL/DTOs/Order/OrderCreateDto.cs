namespace FoodDelivery.BL.DTOs.Order
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}

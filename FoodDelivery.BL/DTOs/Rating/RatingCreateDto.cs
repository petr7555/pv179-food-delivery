using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.Restaurant;

namespace FoodDelivery.BL.DTOs.Rating
{
    public class RatingCreateDto
    {
        public Guid RestaurantId { get; set; }
        public RestaurantGetDto Restaurant { get; set; }

        public Guid OrderId { get; set; }

        public OrderWithProductsGetDto Order { get; set; }

        public int Stars { get; set; }

        public string? Comment { get; set; }
    }
}

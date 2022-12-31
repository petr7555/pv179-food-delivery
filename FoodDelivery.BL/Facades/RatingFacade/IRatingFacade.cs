using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Rating;

namespace FoodDelivery.BL.Facades.RatingFacade
{
    public interface IRatingFacade
    {
        public Task<IEnumerable<RatingGetDto>> GetAllAsync();
        public Task<IEnumerable<RatingGetDto>> QueryAsync(QueryDto<RatingGetDto> queryDto);

        public Task RateRestaurant(RatingCreateDto ratingCreateDto);

        public Task<bool> AlreadyRated(Guid restaurantId, Guid orderId);
    }
}

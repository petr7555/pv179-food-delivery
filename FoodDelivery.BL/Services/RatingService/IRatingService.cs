using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.RatingService
{
    public interface IRatingService : ICrudService<Rating, Guid, RatingGetDto, RatingCreateDto, RatingUpdateDto>
    {
        public Task<IEnumerable<RatingGetDto>> QueryAsync(QueryDto<RatingGetDto> queryDto);

        public Task<bool> AlreadyRated(Guid restaurantId, Guid orderId);
    }
}

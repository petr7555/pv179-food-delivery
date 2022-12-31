using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.RatingService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.RatingFacade
{
    public class RatingFacade : IRatingFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRatingService _ratingService;

        public RatingFacade(IUnitOfWork unitOfWork, IRatingService ratingService)
        {
            _unitOfWork = unitOfWork;
            _ratingService = ratingService;
        }

        public async Task<IEnumerable<RatingGetDto>> GetAllAsync()
        {
            return await _ratingService.GetAllAsync();
        }

        public async Task<IEnumerable<RatingGetDto>> QueryAsync(QueryDto<RatingGetDto> queryDto)
        {
            return await _ratingService.QueryAsync(queryDto);
        }

        public async Task RateRestaurant(RatingCreateDto ratingCreateDto)
        {
            _ratingService.Create(ratingCreateDto);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> AlreadyRated(Guid restaurantId, Guid orderId)
        {
            return await _ratingService.AlreadyRated(restaurantId, orderId);
        }
    }
}

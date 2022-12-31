using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.RatingService
{
    public class RatingService : CrudService<Rating, Guid, RatingGetDto, RatingCreateDto, RatingUpdateDto>, IRatingService
    {
        private readonly IQueryObject<RatingGetDto, Rating> _queryObject;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<RatingGetDto, Rating> queryObject) : base(
            unitOfWork.RatingRepository, mapper)
        {
            _queryObject = queryObject;
        }

        public async Task<IEnumerable<RatingGetDto>> QueryAsync(QueryDto<RatingGetDto> queryDto)
        {
            return await _queryObject.ExecuteAsync(queryDto);
        }

        public async Task<bool> AlreadyRated(Guid restaurantId, Guid orderId)
        {            
            var dto = new QueryDto<RatingGetDto>()
            .Where(rating => rating.RestaurantId.Equals(restaurantId) && rating.OrderId.Equals(orderId));

            var result = await _queryObject.ExecuteAsync(dto);
            return result.Count() > 0;
        }
    }
}

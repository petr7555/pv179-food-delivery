using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.RatingService;

public interface IRatingService : ICrudService<Rating, Guid, RatingGetDto, RatingCreateDto, RatingUpdateDto>
{
}

using AutoMapper;
using FoodDelivery.BL.DTOs.Rating;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.RatingService;

public class RatingService : CrudService<Rating, Guid, RatingGetDto, RatingCreateDto, RatingUpdateDto>,
    IRatingService
{
    public RatingService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.RatingRepository, mapper)
    {
    }
}

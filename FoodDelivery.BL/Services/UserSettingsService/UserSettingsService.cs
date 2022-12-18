using AutoMapper;
using FoodDelivery.BL.DTOs.UserSettings;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.UserSettingsService;

public class UserSettingsService :
    CrudService<UserSettings, Guid, UserSettingsGetDto, UserSettingsCreateDto, UserSettingsUpdateDto>,
    IUserSettingsService
{
    public UserSettingsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.UserSettingsRepository, mapper)
    {
    }
}

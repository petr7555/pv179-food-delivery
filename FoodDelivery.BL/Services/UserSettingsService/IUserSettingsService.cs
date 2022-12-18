using FoodDelivery.BL.DTOs.UserSettings;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.UserSettingsService;

public interface IUserSettingsService : ICrudService<UserSettings, Guid, UserSettingsGetDto, UserSettingsCreateDto, UserSettingsUpdateDto>
{
}

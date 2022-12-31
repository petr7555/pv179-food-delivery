using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.UserService;

public interface IUserService : ICrudService<User, Guid, UserGetDto, UserCreateDto, UserUpdateDto>
{
    public Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto);

    public Task UpdateAddressAsync(Guid userId, Guid addressId, AddressUpdateDto addressUpdateDto);

    public Task BanUserAsync(Guid userId);

    public Task UnbanUserAsync(Guid userId);

    public Task<bool> IsBanned(Guid userId);

    public Task<UserGetDto> GetByUsernameAsync(string username);

    public CustomerDetailsUpdateDto ConvertToUpdateDto(CustomerDetailsGetDto customerDetailsGetDto);
}

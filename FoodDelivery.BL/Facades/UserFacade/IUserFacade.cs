using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.User;

namespace FoodDelivery.BL.Facades.UserFacade;

public interface IUserFacade
{
    public Task<IEnumerable<UserGetDto>> GetAllAsync();

    public Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto);

    public Task UpdateAddressAsync(Guid userId, Guid addressId, AddressUpdateDto addressUpdateDto);

    public Task BanUserAsync(Guid userId);

    public Task UnbanUserAsync(Guid userId);

    public Task CreateUserAsync(UserCreateDto userCreateDto);
}

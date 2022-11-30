using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Services.UserService;

namespace FoodDelivery.BL.Facades;

public class UserFacade : IUserFacade
{
    private readonly IUserService _userService;

    public UserFacade(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IEnumerable<UserGetDto>> GetAllAsync()
    {
        return await _userService.GetAllAsync();
    }

    public async Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto)
    {
        return await _userService.QueryAsync(queryDto);
    }

    public async Task UpdateCustomerDetailsAsync(int userId, CustomerDetailsUpdateDto customerDetailsUpdateDto)
    {
        await _userService.UpdateCustomerDetailsAsync(userId, customerDetailsUpdateDto);
    }

    public async Task UpdateAddressAsync(int userId, int addressId, AddressUpdateDto addressUpdateDto)
    {
        await _userService.UpdateAddressAsync(userId, addressId, addressUpdateDto);
    }

    public async Task BanUserAsync(int userId)
    {
        await _userService.BanUserAsync(userId);
    }

    public async Task UnbanUserAsync(int userId)
    {
        await _userService.UnbanUserAsync(userId);
    }
}

using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.UserFacade;

public class UserFacade : IUserFacade
{
    private readonly IUnitOfWork _uow;
    private readonly IUserService _userService;

    public UserFacade(IUnitOfWork uow, IUserService userService)
    {
        _uow = uow;
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

    public async Task UpdateCustomerDetailsAsync(Guid userId, CustomerDetailsUpdateDto customerDetailsUpdateDto)
    {
        await _userService.UpdateCustomerDetailsAsync(userId, customerDetailsUpdateDto);
    }

    public async Task UpdateAddressAsync(Guid userId, Guid addressId, AddressUpdateDto addressUpdateDto)
    {
        await _userService.UpdateAddressAsync(userId, addressId, addressUpdateDto);
    }

    public async Task BanUserAsync(Guid userId)
    {
        await _userService.BanUserAsync(userId);
    }

    public async Task UnbanUserAsync(Guid userId)
    {
        await _userService.UnbanUserAsync(userId);
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        _userService.Create(userCreateDto);
        await _uow.CommitAsync();
    }
}

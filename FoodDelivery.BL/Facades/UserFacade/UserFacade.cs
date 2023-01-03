using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.DTOs.UserSettings;
using FoodDelivery.BL.Services.AddressService;
using FoodDelivery.BL.Services.CurrencyService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.BL.Services.UserSettingsService;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.UserFacade;

public class UserFacade : IUserFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAddressService _addressService;
    private readonly IUserService _userService;
    private readonly ICurrencyService _currencyService;
    private readonly IUserSettingsService _userSettings;

    public UserFacade(IUnitOfWork unitOfWork, IUserService userService, ICurrencyService currencyService,
        IUserSettingsService userSettingsService, IAddressService addressService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _currencyService = currencyService;
        _userSettings = userSettingsService;
    }

    public async Task<IEnumerable<UserGetDto>> GetAllAsync()
    {
        return await _userService.GetAllAsync();
    }

    public async Task<UserGetDto> GetByUsernameAsync(string username)
    {
        return await _userService.GetByUsernameAsync(username);
    }

    public async Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto)
    {
        return await _userService.QueryAsync(queryDto);
    }

    public async Task UpdateAddressAsync(Guid userId, Guid addressId, AddressUpdateDto addressUpdateDto)
    {
        await _userService.UpdateAddressAsync(userId, addressId, addressUpdateDto);
    }

    public async Task<bool> IsBanned(Guid userId)
    {
        return await _userService.IsBanned(userId);
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
        await _unitOfWork.CommitAsync();
    }

    public async Task<CurrencyGetDto> GetCurrencyAsync(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        return user.UserSettings.SelectedCurrency;
    }

    public async Task<IEnumerable<CurrencyGetDto>> GetRemainingCurrencies(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        var allCurrencies = await _currencyService.GetAllAsync();
        var remainingCurrencies = allCurrencies.Where(c => c.Id != user.UserSettings.SelectedCurrency.Id);
        return remainingCurrencies;
    }

    public async Task SetCurrencyAsync(string username, Guid currencyId)
    {
        var user = await _userService.GetByUsernameAsync(username);

        var userSettingsUpdateDto = new UserSettingsUpdateDto
        {
            Id = user.UserSettings.Id,
            SelectedCurrencyId = currencyId,
        };

        _userSettings.Update(userSettingsUpdateDto, new[] { nameof(UserSettingsUpdateDto.SelectedCurrencyId) });

        await _unitOfWork.CommitAsync();
    }

    public async Task<CurrencyGetDto> GetDefaultCurrencyAsync()
    {
        return await _currencyService.GetDefaultCurrencyAsync();
    }
}

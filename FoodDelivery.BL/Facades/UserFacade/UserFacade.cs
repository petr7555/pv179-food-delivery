using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Services.CurrencyService;
using FoodDelivery.BL.Services.CustomerDetailsService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.UserFacade;

public class UserFacade : IUserFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly ICurrencyService _currencyService;
    private readonly ICustomerDetailsService _customerDetailsService;

    public UserFacade(IUnitOfWork unitOfWork, IUserService userService, ICurrencyService currencyService,
        ICustomerDetailsService customerDetailsService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _currencyService = currencyService;
        _customerDetailsService = customerDetailsService;
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
        await _unitOfWork.CommitAsync();
    }

    public async Task<CurrencyGetDto> GetCurrencyAsync(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        return user.CustomerDetails?.SelectedCurrency ??
               throw new InvalidOperationException("Cannot get currency of user without customer details.");
    }

    public async Task<IEnumerable<CurrencyGetDto>> GetRemainingCurrencies(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        var allCurrencies = await _currencyService.GetAllAsync();
        var remainingCurrencies = allCurrencies.Where(c => c.Id != user.CustomerDetails.SelectedCurrency.Id);
        return remainingCurrencies;
    }

    public async Task SetCurrencyAsync(string username, Guid currencyId)
    {
        var user = await _userService.GetByUsernameAsync(username);
        if (user.CustomerDetails == null)
        {
            throw new InvalidOperationException("Cannot set currency of user without customer details.");
        }

        var customerDetailsUpdateDto = new CustomerDetailsUpdateDto
        {
            Id = user.CustomerDetails.Id,
            SelectedCurrencyId = currencyId,
        };

        _customerDetailsService.Update(customerDetailsUpdateDto,
            new[] { nameof(CustomerDetailsUpdateDto.SelectedCurrencyId) });

        await _unitOfWork.CommitAsync();
    }
}

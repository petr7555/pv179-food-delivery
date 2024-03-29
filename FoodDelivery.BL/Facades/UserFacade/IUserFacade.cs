﻿using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.User;

namespace FoodDelivery.BL.Facades.UserFacade;

public interface IUserFacade
{
    public Task<IEnumerable<UserGetDto>> GetAllAsync();

    public Task<UserGetDto> GetByUsernameAsync(string username);

    public Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto);

    public Task UpdateAddressAsync(Guid userId, Guid addressId, AddressUpdateDto addressUpdateDto);

    public Task<bool> IsBanned(Guid userId);

    public Task BanUserAsync(Guid userId);

    public Task UnbanUserAsync(Guid userId);

    public Task CreateUserAsync(UserCreateDto userCreateDto);

    public Task<CurrencyGetDto> GetCurrencyAsync(string username);

    public Task<IEnumerable<CurrencyGetDto>> GetRemainingCurrencies(string username);

    public Task SetCurrencyAsync(string username, Guid currencyId);

    public Task<CurrencyGetDto> GetDefaultCurrencyAsync();
}

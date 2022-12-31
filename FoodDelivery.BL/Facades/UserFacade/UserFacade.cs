﻿using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Services.AddressService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.UserFacade;

public class UserFacade : IUserFacade
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public UserFacade(IUnitOfWork unitOfWork, IUserService userService, IAddressService addressService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
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

    public async Task BanUserAsync(Guid userId)
    {
        await _userService.BanUserAsync(userId);
    }

    public async Task UnbanUserAsync(Guid userId)
    {
        await _userService.UnbanUserAsync(userId);
    }

    public async Task<bool> IsBanned(Guid userId)
    {
        return await _userService.IsBanned(userId);
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        _userService.Create(userCreateDto);
        await _unitOfWork.CommitAsync();
    }
}

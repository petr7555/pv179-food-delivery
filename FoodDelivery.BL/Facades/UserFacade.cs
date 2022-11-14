using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Services;
using System;
namespace FoodDelivery.BL.Facades
{
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
        public void UpdateCustomerDetails(int userId, CustomerDetailsUpdateDto customerDetailsUpdateDto)
        {
            _userService.UpdateCustomerDetails(userId, customerDetailsUpdateDto);
        }

        public void UpdateAddress(int userId, int addressId, AddressUpdateDto addressUpdateDto)
        {
            _userService.UpdateAddress(userId, addressId, addressUpdateDto);
        }

        
    }
}

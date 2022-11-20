using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services
{
    public interface IUserService : ICrudService<User, int, UserGetDto, UserCreateDto, UserUpdateDto>
    {
        public Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto);

        public Task UpdateCustomerDetailsAsync(int userId, CustomerDetailsUpdateDto customerDetailsUpdateDto);
        public Task UpdateAddressAsync(int userId, int addressId, AddressUpdateDto addressUpdateDto);

        public Task BanUserAsync(int userId);
        public Task UnbanUserAsync(int userId);
    }
}

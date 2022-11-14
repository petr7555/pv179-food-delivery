using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;

namespace FoodDelivery.BL.Facades
{
    public interface IUserFacade
    {
        public Task<IEnumerable<UserGetDto>> GetAllAsync();
        public Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto);
        public void UpdateCustomerDetails(int userId, CustomerDetailsUpdateDto customerDetailsUpdateDto);
        public void UpdateAddress(int userId, int addressId, AddressUpdateDto addressUpdateDto);
    }
}

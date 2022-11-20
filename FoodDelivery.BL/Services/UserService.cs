using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services
{
    public class UserService : CrudService<User, int, UserGetDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.UserRepository, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto)
        {
            var queryObject = new QueryObject<UserGetDto, User>(Mapper, _unitOfWork.UserQuery);
            return await queryObject.ExecuteAsync(queryDto);
        }

        public async void UpdateCustomerDetails(int userId, CustomerDetailsUpdateDto customerDetailsUpdateDto)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            user.CustomerDetails = Mapper.Map<CustomerDetails>(customerDetailsUpdateDto);
            _unitOfWork.UserRepository.Update(user);
        }

        public async void UpdateAddress(int userId, int addressId, AddressUpdateDto addressUpdateDto)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user.CustomerDetails.BillingAddressId == addressId)
            {
                user.CustomerDetails.BillingAddress = Mapper.Map<Address>(addressUpdateDto);
            } else if (user.CustomerDetails.DeliveryAddressId == addressId)
            {
                user.CustomerDetails.DeliveryAddress = Mapper.Map<Address>(addressUpdateDto);
            }

            _unitOfWork.UserRepository.Update(user);
        }

        public async void BanUser(int userId)
        {
            // Will be reworked based on authentication and authorization

            // check privileges

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            user.Banned = true;
            _unitOfWork.UserRepository.Update(user);
        }

        public async void UnbanUser(int userId)
        {
            // Will be reworked based on authentication and authorization

            // check privileges

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            user.Banned = false;
            _unitOfWork.UserRepository.Update(user);
        }
    }
}

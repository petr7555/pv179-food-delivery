using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.UserService;

public class UserService : CrudService<User, Guid, UserGetDto, UserCreateDto, UserUpdateDto>, IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IQueryObject<UserGetDto, User> _queryObject;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<UserGetDto, User> queryObject) : base(
        unitOfWork.UserRepository, mapper)
    {
        _unitOfWork = unitOfWork;
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<UserGetDto>> QueryAsync(QueryDto<UserGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }

    public async Task UpdateCustomerDetailsAsync(Guid userId, CustomerDetailsUpdateDto customerDetailsUpdateDto)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        user.CustomerDetails = Mapper.Map<CustomerDetails>(customerDetailsUpdateDto);
        // TODO
        _unitOfWork.UserRepository.Update(user, Array.Empty<string>());
    }

    public async Task UpdateAddressAsync(Guid userId, Guid addressId, AddressUpdateDto addressUpdateDto)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        if (user.CustomerDetails.BillingAddressId == addressId)
        {
            user.CustomerDetails.BillingAddress = Mapper.Map<Address>(addressUpdateDto);
        }
        else if (user.CustomerDetails.DeliveryAddressId == addressId)
        {
            user.CustomerDetails.DeliveryAddress = Mapper.Map<Address>(addressUpdateDto);
        }

        // TODO
        _unitOfWork.UserRepository.Update(user, Array.Empty<string>());
    }

    public async Task BanUserAsync(Guid userId)
    {
        // TODO Will be reworked based on authentication and authorization

        // TODO check privileges

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        user.Banned = true;
        _unitOfWork.UserRepository.Update(user, new []{nameof(User.Banned)});
    }

    public async Task UnbanUserAsync(Guid userId)
    {
        // TODO Will be reworked based on authentication and authorization

        // TODO check privileges

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        user.Banned = false;
        _unitOfWork.UserRepository.Update(user, new []{nameof(User.Banned)});
    }

    public async Task<UserGetDto> GetByUsernameAsync(string username)
    {
        var user = (await QueryAsync(new QueryDto<UserGetDto>().Where(u => u.Email == username))).Single();
        return user;
    }
}

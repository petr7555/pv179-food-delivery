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

    public async Task UpdateAddressAsync(Guid userId, Guid addressId, AddressUpdateDto addressUpdateDto)
    {
        addressUpdateDto.Id = addressId;
        _unitOfWork.AddressRepository.Update(
            Mapper.Map<Address>(addressUpdateDto),
            new[]
            {
                nameof(Address.FullName),
                nameof(Address.StreetAddress),
                nameof(Address.City),
                nameof(Address.State),
                nameof(Address.ZipCode),
                nameof(Address.PhoneNumber)
            });
        await _unitOfWork.CommitAsync();
    }

    public async Task<bool> IsBanned(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        return user.Banned;
    }

    public async Task BanUserAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        user.Banned = true;
        _unitOfWork.UserRepository.Update(user, new[] { nameof(User.Banned) });
        await _unitOfWork.CommitAsync();
    }

    public async Task UnbanUserAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        user.Banned = false;
        _unitOfWork.UserRepository.Update(user, new[] { nameof(User.Banned) });
        await _unitOfWork.CommitAsync();
    }

    public async Task<UserGetDto> GetByUsernameAsync(string username)
    {
        var user = (await QueryAsync(new QueryDto<UserGetDto>().Where(u => u.Email == username))).Single();
        return user;
    }

    public CustomerDetailsUpdateDto ConvertToUpdateDto(CustomerDetailsGetDto customerDetailsGetDto)
    {
        return Mapper.Map<CustomerDetailsUpdateDto>(Mapper.Map<CustomerDetails>(customerDetailsGetDto));
    }
}

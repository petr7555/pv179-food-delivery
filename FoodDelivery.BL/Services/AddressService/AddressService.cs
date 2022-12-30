using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.AddressService;

public class AddressService : CrudService<Address, Guid, AddressGetDto, AddressCreateDto, AddressUpdateDto>, IAddressService
{
    private readonly IQueryObject<AddressGetDto, Address> _queryObject;

    public AddressService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<AddressGetDto, Address> queryObject) : base(
        unitOfWork.AddressRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<AddressGetDto>> QueryAsync(QueryDto<AddressGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }
}

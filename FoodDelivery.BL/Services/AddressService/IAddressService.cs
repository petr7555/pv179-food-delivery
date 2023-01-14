using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.AddressService;

public interface IAddressService : ICrudService<Address, Guid, AddressGetDto, AddressCreateDto, AddressUpdateDto>
{
}

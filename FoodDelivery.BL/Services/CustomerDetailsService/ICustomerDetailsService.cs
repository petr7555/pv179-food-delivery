using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.CustomerDetailsService;

public interface ICustomerDetailsService : ICrudService<CustomerDetails, Guid, CustomerDetailsGetDto, CustomerDetailsCreateDto, CustomerDetailsUpdateDto>
{
}

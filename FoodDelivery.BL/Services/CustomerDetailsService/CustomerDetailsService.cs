using AutoMapper;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.CustomerDetailsService;

public class CustomerDetailsService : CrudService<CustomerDetails, Guid, CustomerDetailsGetDto, CustomerDetailsCreateDto
        , CustomerDetailsUpdateDto>,
    ICustomerDetailsService
{
    public CustomerDetailsService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.CustomerDetailsRepository,
        mapper)
    {
    }
}

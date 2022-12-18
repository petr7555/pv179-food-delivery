using AutoMapper;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.CurrencyService;

public class CurrencyService : CrudService<Currency, Guid, CurrencyGetDto, CurrencyCreateDto, CurrencyUpdateDto>,
    ICurrencyService
{
    public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.CurrencyRepository, mapper)
    {
    }
}

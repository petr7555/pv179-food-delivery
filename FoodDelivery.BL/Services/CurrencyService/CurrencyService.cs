using AutoMapper;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.CurrencyService;

public class CurrencyService : CrudService<Currency, Guid, CurrencyGetDto, CurrencyCreateDto, CurrencyUpdateDto>,
    ICurrencyService
{
    private const string DefaultCurrencyName = "CZK";

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.CurrencyRepository, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CurrencyGetDto> GetDefaultCurrencyAsync()
    {
        var currency = await _unitOfWork.CurrencyRepository.GetAllAsync();
        return _mapper.Map<CurrencyGetDto>(currency.Single(c => c.Name == DefaultCurrencyName));
    }
}

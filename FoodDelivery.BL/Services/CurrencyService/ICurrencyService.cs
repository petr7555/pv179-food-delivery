using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.CurrencyService;

public interface ICurrencyService : ICrudService<Currency, Guid, CurrencyGetDto, CurrencyCreateDto, CurrencyUpdateDto>
{
    public Task<CurrencyGetDto> GetDefaultCurrencyAsync();
}

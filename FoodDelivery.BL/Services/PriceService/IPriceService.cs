using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.PriceService;

public interface IPriceService : ICrudService<Price, Guid, PriceGetDto, PriceCreateDto, PriceUpdateDto>
{
    public Task<IEnumerable<PriceCreateDto>> GetAllAsCreateDtoAsync();
    public Task<IEnumerable<PriceGetDto>> QueryAsync(QueryDto<PriceGetDto> queryDto);
    public Task<IEnumerable<CurrencyGetDto>> GetAllCurrencies();

    public void Create(PriceUpdateDto priceUpdateDto);
    public void Update(PriceCreateDto priceCreateDto);

    public PriceCreateDto ConvertToCreateDto(PriceGetDto priceGetDto);
}
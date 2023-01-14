using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.PriceService;

public class PriceService :
    CrudService<Price, Guid, PriceGetDto, PriceCreateDto, PriceUpdateDto>,
    IPriceService
{
    private readonly IQueryObject<PriceGetDto, Price> _queryObject;

    public PriceService(IUnitOfWork unitOfWork, IMapper mapper,
        IQueryObject<PriceGetDto, Price> queryObject) : base(unitOfWork.PriceRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<PriceCreateDto>> GetAllAsCreateDtoAsync()
    {
        var allPrices = await GetAllAsync();
        return allPrices.Select(price => Mapper.Map<PriceCreateDto>(Mapper.Map<Price>(price)));
    }

    public async Task<IEnumerable<PriceGetDto>> QueryAsync(QueryDto<PriceGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }

    public void Create(PriceUpdateDto priceUpdateDto)
    {
        Create(Mapper.Map<PriceCreateDto>(Mapper.Map<Price>(priceUpdateDto)));
    }

    public void Update(PriceCreateDto priceCreateDto)
    {
        Update(
            Mapper.Map<PriceUpdateDto>(Mapper.Map<Price>(priceCreateDto)),
            new[]
            {
                nameof(Price.Amount)
            });
    }

    public async Task<IEnumerable<CurrencyGetDto>> GetAllCurrencies()
    {
        var allPrices = await GetAllAsync();
        return allPrices.Select(p => p.Currency).GroupBy(x => x.Name).Select(g => g.First()).ToList();
    }

    public PriceCreateDto ConvertToCreateDto(PriceGetDto priceGetDto)
    {
        return Mapper.Map<PriceCreateDto>(Mapper.Map<Price>(priceGetDto));
    }
}

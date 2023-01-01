using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Repository;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.PriceService;
public class PriceService :
    CrudService<Price, Guid, PriceGetDto, PriceCreateDto, PriceGetDto>,
    IPriceService
{
    private readonly IQueryObject<PriceGetDto, Price> _queryObject;

    public PriceService(IUnitOfWork unitOfWork, IMapper mapper,
        IQueryObject<PriceGetDto, Price> queryObject) : base(unitOfWork.PriceRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<PriceGetDto>> QueryAsync(QueryDto<PriceGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }

    public async Task<IEnumerable<CurrencyGetDto>> GetAllCurrencies()
    {
        var allPrices = await GetAllAsync();
        return allPrices.Select(p => p.Currency).GroupBy(x => x.Name).Select(g => g.First()).ToList();
    }
}

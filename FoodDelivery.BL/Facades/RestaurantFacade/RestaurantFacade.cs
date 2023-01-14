using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services.PriceService;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.BL.Services.RestaurantService;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Facades.RestaurantFacade;

public class RestaurantFacade : IRestaurantFacade
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPriceService _priceService;
    private readonly IProductService _productService;
    private readonly IRestaurantService _restaurantService;

    public RestaurantFacade(IUnitOfWork unitOfWork, IPriceService priceService, IProductService productService,
        IRestaurantService restaurantService)
    {
        _unitOfWork = unitOfWork;
        _priceService = priceService;
        _productService = productService;
        _restaurantService = restaurantService;
    }

    public async Task<IEnumerable<RestaurantGetDto>> GetAllAsync()
    {
        return await _restaurantService.GetAllAsync();
    }

    public async Task<RestaurantGetDto> GetByIdAsync(Guid restaurantId)
    {
        return await _restaurantService.GetByIdAsync(restaurantId);
    }

    public async Task<IEnumerable<RestaurantGetDto>> QueryAsync(QueryDto<RestaurantGetDto> queryDto)
    {
        return await _restaurantService.QueryAsync(queryDto);
    }

    public async Task CreateAsync(RestaurantCreateDto dto)
    {
        _restaurantService.Create(dto);
        await _unitOfWork.CommitAsync();
    }

    public async Task CreateWithNewPrices(RestaurantCreateDto restaurantCreateDto,
        IEnumerable<PriceCreateDto> priceCreateDtos)
    {
        _restaurantService.Create(restaurantCreateDto);

        for (int i = 0; i < priceCreateDtos.Count(); i++)
        {
            _priceService.Create(priceCreateDtos.ElementAt(i));
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(RestaurantCreateDto restaurantCreateDto, List<PriceCreateDto> priceCreateDtos)
    {
        foreach (var priceCreateDto in priceCreateDtos)
        {
            _priceService.Update(priceCreateDto);
        }

        _restaurantService.Update(restaurantCreateDto);
        await _unitOfWork.CommitAsync();
    }

    public async Task Delete(Guid restaurantId)
    {
        var products = await _productService.GetAllAsync();
        products = products.Where((product) => product.Restaurant.Id == restaurantId);
        foreach (var product in products)
        {
            foreach (var price in product.Prices)
            {
                _priceService.Delete(price.Id);
            }

            _productService.Delete(product.Id);
        }

        var restaurant = await GetByIdAsync(restaurantId);
        _restaurantService.Delete(restaurantId);
        _unitOfWork.Commit();
    }

    public RestaurantCreateDto ConvertToCreateDto(RestaurantGetDto restaurantGetDto)
    {
        return _restaurantService.ConvertToCreateDto(restaurantGetDto);
    }
}

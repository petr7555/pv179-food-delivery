
using System.Drawing.Printing;
using AutoMapper;
using FluentAssertions;
using FoodDelivery.BL.Configs;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Repository;
using FoodDelivery.Infrastructure.UnitOfWork;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace FoodDelivery.BL.Test;

public class RestaurantServiceTest
{
    private readonly RestaurantService _service;
    private readonly Mapper _mapper;
    private readonly Currency _czkCurrency;
    private readonly Restaurant _restaurantGiuseppe;
    private readonly Restaurant _restaurantOhPho;
    private readonly List<Restaurant> _allRestautants;
    private List<Restaurant> _mockDb;

    public RestaurantServiceTest()
    {
        // Repository
        var repository = Mock.Create<IRepository<Restaurant, int>>();

        _czkCurrency = new Currency { Id = 1, Name = "CZK" };
        var deliveryPrice30 = new Price { Id = 1, Amount = 30, CurrencyId = _czkCurrency.Id };
        var deliveryPrice50 = new Price { Id = 2, Amount = 50, CurrencyId = _czkCurrency.Id };
        _restaurantGiuseppe = new Restaurant
            { Id = 1, Name = "Pizza Giuseppe", DeliveryPriceId = deliveryPrice30.Id };
        _restaurantOhPho = new Restaurant
            { Id = 2, Name = "Oh Pho", DeliveryPriceId = deliveryPrice50.Id };
        _allRestautants = new List<Restaurant>
        {
            _restaurantGiuseppe,
            _restaurantOhPho
        };
        _mockDb = new List<Restaurant>();

        Mock.Arrange(() => repository.GetByIdAsync(1))
            .ReturnsAsync(() => _restaurantGiuseppe);
        Mock.Arrange(() => repository.GetByIdAsync(2))
            .ReturnsAsync(() => _restaurantOhPho);
        Mock.Arrange(() => repository.GetAllAsync())
            .ReturnsAsync(() => _allRestautants);
        Mock.Arrange(() => repository.Create(Arg.IsAny<Restaurant>()))
            .DoInstead((Restaurant r) => _mockDb.Add(r));
        Mock.Arrange(() => repository.Update(Arg.IsAny<Restaurant>()))
            .DoInstead((Restaurant r) =>
            {
                var i = _mockDb.FindIndex(rr => r.Id == rr.Id);
                _mockDb[i] = r;
            });
        Mock.Arrange(() => repository.Delete(Arg.IsAny<int>()))
            .DoInstead((int i) => _mockDb.RemoveAll(r => r.Id == i));
        
        // UnitOfWork
        var unitOfWork = Mock.Create<IUnitOfWork>();
        Mock.Arrange(() => unitOfWork.RestaurantRepository)
            .Returns(repository);

        _mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        _service = new RestaurantService(unitOfWork, _mapper);
    }
    
    
    [Fact]
    public void ItGetsByIdAsync()
    {
        var giuseppe = _service.GetByIdAsync(1).Result;
        giuseppe.Should().BeEquivalentTo(_mapper.Map<RestaurantGetDto>(_restaurantGiuseppe));

        var ohPho = _service.GetByIdAsync(2).Result;
        ohPho.Should().BeEquivalentTo(_mapper.Map<RestaurantGetDto>(_restaurantOhPho));
    }

    [Fact]
    public void ItGetsAllAsync()
    {
        var allRestaurants = _service.GetAllAsync().Result;
        allRestaurants.Should()
            .BeEquivalentTo(_allRestautants.Select(r => _mapper.Map<RestaurantGetDto>(r)));
    }

    private Restaurant getTestRestaurant()
    {
        var deliveryPrice = new Price { Id = 1, Amount = 40, CurrencyId = _czkCurrency.Id };
        return new Restaurant
            { Id = 1, Name = "Bastardo Mexico", DeliveryPriceId = deliveryPrice.Id };
    }
    
    [Fact]
    public void ItCreates()
    {
        var restaurant = getTestRestaurant();
        _service.Create(_mapper.Map<RestaurantCreateDto>(restaurant));
        Assert.Contains(_mockDb, 
            r => r.Id == restaurant.Id
                          && r.Name == restaurant.Name 
                          && r.DeliveryPriceId == restaurant.DeliveryPriceId
                          && r.DeliveryPrice == restaurant.DeliveryPrice);
        _mockDb.Clear();
    }
    
    [Fact]
    public void ItUpdates()
    {
        var restaurant = getTestRestaurant();
        _mockDb.Add(restaurant);
        var updatedRestaurant = getTestRestaurant();
        updatedRestaurant.Name = "Updated Name";
        _service.Update(_mapper.Map<RestaurantUpdateDto>(updatedRestaurant));
        Assert.Contains(_mockDb, 
            r => r.Id == updatedRestaurant.Id
                 && r.Name == updatedRestaurant.Name 
                 && r.DeliveryPriceId == updatedRestaurant.DeliveryPriceId
                 && r.DeliveryPrice == updatedRestaurant.DeliveryPrice);
        _mockDb.Clear();
    }
    
    [Fact]
    public void ItDeletes()
    {
        var restaurant = getTestRestaurant();
        _mockDb.Add(restaurant);
        _service.Delete(restaurant.Id);
        Assert.DoesNotContain(_mockDb, r => r.Id == restaurant.Id);
    }
}
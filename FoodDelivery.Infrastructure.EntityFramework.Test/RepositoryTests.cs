using FluentAssertions;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Query;
using FoodDelivery.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Assert = Xunit.Assert;

namespace FoodDelivery.Infrastructure.EntityFramework.Test;

public class RepositoryTests
{
    private readonly FoodDeliveryDbContext _dbContext;
    private readonly Price price;
    private readonly EfRepository<Restaurant, int> efRestaurantRepository;

    public RepositoryTests()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();

        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        _dbContext = new FoodDeliveryDbContext(dbContextOptions);

        efRestaurantRepository = new EfRepository<Restaurant, int>(_dbContext);

        var czkCurrency = new Currency { Id = 1, Name = "CZK" };
        _dbContext.Currencies.Add(czkCurrency);
        price = new Price { Id = 1, Amount = 50, CurrencyId = czkCurrency.Id };
        _dbContext.Prices.Add(price);

        _dbContext.Restaurants.Add(new Restaurant
            { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 4, Name = "Steak House K1", DeliveryPriceId = price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 5, Name = "Jean Paul's", DeliveryPriceId = price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id }
        );

        _dbContext.SaveChanges();
    }

    [Fact]
    public void ItGetsValidRestaurant()
    {
        var expectedRestaurant = new Restaurant
            { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price };
        var result = efRestaurantRepository.GetByIdAsync(6).Result;

        result.Should().BeEquivalentTo(expectedRestaurant);
    }

    [Fact]
    public void ItGetsAllRestaurants()
    {
        var expectedRestaurants = new List<Restaurant>
        {
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 4, Name = "Steak House K1", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 5, Name = "Jean Paul's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price }
        };

        var result = efRestaurantRepository.GetAllAsync().Result;

        result.Should().BeEquivalentTo(expectedRestaurants);
    }

    [Fact]
    public void ItCreatesRestaurant()
    {
        var restaurant = new Restaurant
            { Id = 7, Name = "Pizza test", DeliveryPriceId = price.Id, DeliveryPrice = price };
        efRestaurantRepository.Create(restaurant);

        var found = _dbContext.Restaurants.Find(7);
        found.Should().BeEquivalentTo(restaurant);

        // clean up
        _dbContext.Restaurants.Remove(restaurant);
    }

    [Fact]
    public void ItUpdatesValidRestaurant()
    {
        var restaurant = new Restaurant
            { Id = 7, Name = "Pizza test", DeliveryPriceId = price.Id, DeliveryPrice = price };
        _dbContext.Restaurants.Add(restaurant);

        restaurant.Name = "Changed pizza test";

        efRestaurantRepository.Update(restaurant);

        var found = _dbContext.Restaurants.Find(7);
        found.Should().BeEquivalentTo(restaurant);

        // clean up
        _dbContext.Restaurants.Remove(restaurant);
    }

    [Fact]
    public void ItDeletesValidRestaurant()
    {
        var restaurant = new Restaurant
            { Id = 7, Name = "Pizza test", DeliveryPriceId = price.Id, DeliveryPrice = price };
        _dbContext.Restaurants.Add(restaurant);

        efRestaurantRepository.Delete(7);

        Assert.False(_dbContext.Restaurants.Contains(restaurant));
    }


    [Fact]
    public void ItGetsInvalidRestaurant()
    {
        var result = efRestaurantRepository.GetByIdAsync(7).Result;

        result.Should().BeNull();
    }

    [Fact]
    public void ItCreatesNullRestaurant()
    {
        Assert.Throws<ArgumentNullException>(() => efRestaurantRepository.Create(null));
    }

    [Fact]
    public void ItUpdatesInvalidRestaurant()
    {
        Assert.Throws<ArgumentNullException>(() => efRestaurantRepository.Update(null));
    }

    [Fact]
    public void ItDeletesInvalidRestaurant()
    {
        var originalAmount = _dbContext.Restaurants.Count();
        efRestaurantRepository.Delete(8);
        var updatedAmount = _dbContext.Restaurants.Count();
        updatedAmount.Should().Be(originalAmount);
    }
}

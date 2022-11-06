using FluentAssertions;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.Test;

public class RepositoryTests
{
    private readonly FoodDeliveryDbContext _dbContext;
    private readonly Price _price;
    private readonly EfRepository<Restaurant, int> _repository;

    public RepositoryTests()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();

        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        _dbContext = new FoodDeliveryDbContext(dbContextOptions);

        _repository = new EfRepository<Restaurant, int>(_dbContext);

        var czkCurrency = new Currency { Id = 1, Name = "CZK" };
        _dbContext.Currencies.Add(czkCurrency);
        _price = new Price { Id = 1, Amount = 50, CurrencyId = czkCurrency.Id };
        _dbContext.Prices.Add(_price);

        _dbContext.Restaurants.Add(new Restaurant
            { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = _price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = _price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 3, Name = "Pizza Hut", DeliveryPriceId = _price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 4, Name = "Steak House K1", DeliveryPriceId = _price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 5, Name = "Jean Paul's", DeliveryPriceId = _price.Id }
        );
        _dbContext.Restaurants.Add(new Restaurant
            { Id = 6, Name = "POE POE", DeliveryPriceId = _price.Id }
        );

        _dbContext.SaveChanges();
    }

    [Fact]
    public void ItGetsRestaurantThatExistsById()
    {
        var expectedRestaurant = new Restaurant
            { Id = 6, Name = "POE POE", DeliveryPriceId = _price.Id, DeliveryPrice = _price };
        var result = _repository.GetByIdAsync(6).Result;

        result.Should().BeEquivalentTo(expectedRestaurant);
    }

    [Fact]
    public void ItGetsAllRestaurants()
    {
        var expectedRestaurants = new List<Restaurant>
        {
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = _price.Id, DeliveryPrice = _price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = _price.Id, DeliveryPrice = _price },
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = _price.Id, DeliveryPrice = _price },
            new() { Id = 4, Name = "Steak House K1", DeliveryPriceId = _price.Id, DeliveryPrice = _price },
            new() { Id = 5, Name = "Jean Paul's", DeliveryPriceId = _price.Id, DeliveryPrice = _price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = _price.Id, DeliveryPrice = _price }
        };

        var result = _repository.GetAllAsync().Result;

        result.Should().BeEquivalentTo(expectedRestaurants);
    }

    [Fact]
    public void ItCreatesRestaurant()
    {
        var restaurant = new Restaurant
            { Id = 7, Name = "Pizza test", DeliveryPriceId = _price.Id, DeliveryPrice = _price };
        _repository.Create(restaurant);

        var found = _dbContext.Restaurants.Find(7);
        found.Should().BeEquivalentTo(restaurant);
    }

    [Fact]
    public void ItUpdatesRestaurantThatExists()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();
        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        using (var dbContext = new FoodDeliveryDbContext(dbContextOptions))
        {
            dbContext.Restaurants.Add(new Restaurant
                { Id = 10, Name = "Pizza test", DeliveryPriceId = _price.Id, DeliveryPrice = _price });
            dbContext.SaveChanges();
        }

        using (var dbContext = new FoodDeliveryDbContext(dbContextOptions))
        {
            var repository = new EfRepository<Restaurant, int>(dbContext);
            repository.Update(new Restaurant
                { Id = 10, Name = "Updated pizza test", DeliveryPriceId = _price.Id, DeliveryPrice = _price });
            dbContext.Restaurants.Find(10)!.Name.Should().BeEquivalentTo("Updated pizza test");
        }
    }

    [Fact]
    public void ItDeletesRestaurantThatExists()
    {
        _repository.Delete(1);
        _dbContext.SaveChanges();

        _dbContext.Restaurants.Find(1).Should().BeNull();
    }


    [Fact]
    public void ItReturnsNullWhenRestaurantDoesNotExist()
    {
        var result = _repository.GetByIdAsync(7).Result;

        result.Should().BeNull();
    }

    [Fact]
    public void ItThrowsAnExceptionWhenCreatingNullRestaurant()
    {
        Assert.Throws<ArgumentNullException>(() => _repository.Create(null));
    }

    [Fact]
    public void ItThrowsAnExceptionWhenUpdatingNullRestaurant()
    {
        Assert.Throws<ArgumentNullException>(() => _repository.Update(null));
    }

    [Fact]
    public void ItDoesNothingWhenAttemptingToDeleteNonexistentRestaurant()
    {
        var numRestaurantsBeforeDelete = _dbContext.Restaurants.Count();
        _repository.Delete(8);
        var numRestaurantsAfterDelete = _dbContext.Restaurants.Count();
        numRestaurantsAfterDelete.Should().Be(numRestaurantsBeforeDelete);
    }
}

using FluentAssertions;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.Test;

public class RepositoryTests
{
    private readonly FoodDeliveryDbContext _context;
    private readonly Price _price;
    private readonly EfRepository<Restaurant, int> _repository;

    public RepositoryTests()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();

        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        _context = new FoodDeliveryDbContext(dbContextOptions);

        _repository = new EfRepository<Restaurant, int>(_context);

        var czkCurrency = new Currency { Id = 1, Name = "CZK" };
        _context.Currencies.Add(czkCurrency);
        _price = new Price { Id = 1, Amount = 50, CurrencyId = czkCurrency.Id };
        _context.Prices.Add(_price);

        _context.Restaurants.Add(new Restaurant
            { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = _price.Id }
        );
        _context.Restaurants.Add(new Restaurant
            { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = _price.Id }
        );
        _context.Restaurants.Add(new Restaurant
            { Id = 3, Name = "Pizza Hut", DeliveryPriceId = _price.Id }
        );
        _context.Restaurants.Add(new Restaurant
            { Id = 4, Name = "Steak House K1", DeliveryPriceId = _price.Id }
        );
        _context.Restaurants.Add(new Restaurant
            { Id = 5, Name = "Jean Paul's", DeliveryPriceId = _price.Id }
        );
        _context.Restaurants.Add(new Restaurant
            { Id = 6, Name = "POE POE", DeliveryPriceId = _price.Id }
        );

        _context.SaveChanges();
    }

    [Fact]
    public async Task ItGetsRestaurantThatExistsById()
    {
        var expectedRestaurant = new Restaurant
            { Id = 6, Name = "POE POE", DeliveryPriceId = _price.Id, DeliveryPrice = _price };
        var result = await _repository.GetByIdAsync(6);

        result.Should().BeEquivalentTo(expectedRestaurant);
    }

    [Fact]
    public async Task ItGetsAllRestaurants()
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

        var result = await _repository.GetAllAsync();

        result.Should().BeEquivalentTo(expectedRestaurants);
    }

    [Fact]
    public void ItCreatesRestaurant()
    {
        var restaurant = new Restaurant
            { Id = 7, Name = "Pizza test", DeliveryPriceId = _price.Id, DeliveryPrice = _price };
        _repository.Create(restaurant);

        var found = _context.Restaurants.Find(7);
        found.Should().BeEquivalentTo(restaurant);
    }

    [Fact]
    public void ItUpdatesRestaurantThatExists()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();
        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        using (var context = new FoodDeliveryDbContext(dbContextOptions))
        {
            context.Restaurants.Add(new Restaurant
                { Id = 10, Name = "Pizza test", DeliveryPriceId = _price.Id, DeliveryPrice = _price });
            context.SaveChanges();
        }

        using (var context = new FoodDeliveryDbContext(dbContextOptions))
        {
            var repository = new EfRepository<Restaurant, int>(context);
            repository.Update(new Restaurant
                { Id = 10, Name = "Updated pizza test", DeliveryPriceId = _price.Id, DeliveryPrice = _price });
            context.Restaurants.Find(10)!.Name.Should().BeEquivalentTo("Updated pizza test");
        }
    }

    [Fact]
    public void ItDeletesRestaurantThatExists()
    {
        _repository.Delete(1);
        _context.SaveChanges();

        _context.Restaurants.Find(1).Should().BeNull();
    }


    [Fact]
    public async Task ItReturnsNullWhenRestaurantDoesNotExist()
    {
        var result = await _repository.GetByIdAsync(7);

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
        var numRestaurantsBeforeDelete = _context.Restaurants.Count();
        _repository.Delete(8);
        var numRestaurantsAfterDelete = _context.Restaurants.Count();
        numRestaurantsAfterDelete.Should().Be(numRestaurantsBeforeDelete);
    }
}

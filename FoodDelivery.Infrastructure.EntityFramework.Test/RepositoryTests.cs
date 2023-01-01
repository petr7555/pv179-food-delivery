using FluentAssertions;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.Test;

public class RepositoryTests
{
    private readonly FoodDeliveryDbContext _context;
    private readonly EfRepository<Restaurant, Guid> _repository;

    private readonly Price _priceFifty;
    private readonly Restaurant _pizzaGiuseppe;
    private readonly Restaurant _pizzaDominos;
    private readonly Restaurant _pizzaHut;
    private readonly Restaurant _k1;
    private readonly Restaurant _jeanPauls;
    private readonly Restaurant _poePoe;

    public RepositoryTests()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();

        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        _context = new FoodDeliveryDbContext(dbContextOptions);

        _repository = new EfRepository<Restaurant, Guid>(_context);

        var czkCurrency = new Currency { Id = Guid.NewGuid(), Name = "CZK" };
        _context.Currencies.Add(czkCurrency);

        _priceFifty = new Price { Id = Guid.NewGuid(), Amount = 50, CurrencyId = czkCurrency.Id };
        var priceEighty = new Price { Id = Guid.NewGuid(), Amount = 80, CurrencyId = czkCurrency.Id };
        _context.Prices.AddRange(_priceFifty, priceEighty);

        _pizzaGiuseppe = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Pizza Guiseppe",
            DeliveryPriceId = _priceFifty.Id,
        };
        _pizzaDominos = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Pizza Domino's",
            DeliveryPriceId = _priceFifty.Id
        };
        _pizzaHut = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Pizza Hut",
            DeliveryPriceId = priceEighty.Id
        };
        _k1 = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Steak House K1",
            DeliveryPriceId = priceEighty.Id
        };
        _jeanPauls = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Jean Paul's",
            DeliveryPriceId = priceEighty.Id
        };
        _poePoe = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "POE POE",
            DeliveryPriceId = priceEighty.Id
        };
        _context.Restaurants.AddRange(_pizzaGiuseppe, _pizzaDominos, _pizzaHut, _k1, _jeanPauls, _poePoe);

        _context.SaveChanges();
    }

    [Fact]
    public async Task ItGetsRestaurantThatExistsById()
    {
        var result = await _repository.GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000006"));

        _poePoe.Should().BeNull();

        result.Should().BeEquivalentTo(_poePoe);
    }

    [Fact]
    public async Task ItGetsAllRestaurants()
    {
        var result = await _repository.GetAllAsync();

        result.Should().BeEmpty();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaGiuseppe,
            _pizzaDominos,
            _pizzaHut,
            _k1,
            _jeanPauls,
            _poePoe,
        });
    }

    [Fact]
    public void ItCreatesRestaurant()
    {
        var id = Guid.Parse("00000000-0000-0000-0000-000000000007");
        var restaurant = new Restaurant
            { Id = id, Name = "Pizza test", DeliveryPriceId = _priceFifty.Id, DeliveryPrice = _priceFifty };
        _repository.Create(restaurant);

        var found = _context.Restaurants.Find(id);

        found.Should().BeEquivalentTo(restaurant);
    }

    [Fact]
    public void ItUpdatesRestaurantThatExists()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();
        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        var id = Guid.Parse("00000000-0000-0000-0000-000000000010");
        using (var context = new FoodDeliveryDbContext(dbContextOptions))
        {
            context.Restaurants.Add(new Restaurant
                { Id = id, Name = "Pizza test", DeliveryPriceId = _priceFifty.Id, DeliveryPrice = _priceFifty });
            context.SaveChanges();
        }

        using (var context = new FoodDeliveryDbContext(dbContextOptions))
        {
            var repository = new EfRepository<Restaurant, Guid>(context);
            repository.Update(new Restaurant
            {
                Id = id, Name = "Updated pizza test", DeliveryPriceId = _priceFifty.Id, DeliveryPrice = _priceFifty
            });

            context.Restaurants.Find(id)!.Name.Should().BeEquivalentTo("Updated pizza test");
        }
    }

    [Fact]
    public void ItDeletesRestaurantThatExists()
    {
        var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
        _repository.Delete(id);
        _context.SaveChanges();

        _context.Restaurants.Find(id).Should().BeNull();
    }


    [Fact]
    public async Task ItReturnsNullWhenRestaurantDoesNotExist()
    {
        var result = await _repository.GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000007"));

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
        _repository.Delete(Guid.Parse("00000000-0000-0000-0000-000000000008"));
        var numRestaurantsAfterDelete = _context.Restaurants.Count();
        numRestaurantsAfterDelete.Should().Be(numRestaurantsBeforeDelete);
    }
}

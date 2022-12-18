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
        _context.Prices.AddRange(_priceFifty);

        _pizzaGiuseppe = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Pizza Guiseppe",
            DeliveryPrices = new List<Price>(),
        };
        _pizzaDominos = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Pizza Domino's",
            DeliveryPrices = new List<Price>(),
        };
        _pizzaHut = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Pizza Hut",
            DeliveryPrices = new List<Price>(),
        };
        _k1 = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Steak House K1",
            DeliveryPrices = new List<Price>(),
        };
        _jeanPauls = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Jean Paul's",
            DeliveryPrices = new List<Price>(),
        };
        _poePoe = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "POE POE",
            DeliveryPrices = new List<Price>(),
        };
        _context.Restaurants.AddRange(_pizzaGiuseppe, _pizzaDominos, _pizzaHut, _k1, _jeanPauls, _poePoe);

        _context.SaveChanges();
    }

    [Fact]
    public async Task ItGetsRestaurantThatExistsById()
    {
        var result = await _repository.GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000006"));

        result.Should().BeEquivalentTo(_poePoe);
    }

    [Fact]
    public async Task ItGetsAllRestaurants()
    {
        var result = await _repository.GetAllAsync();

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
        var restaurant = new Restaurant { Id = id, Name = "Pizza test" };
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
            context.Restaurants.Add(new Restaurant { Id = id, Name = "Pizza test" });
            context.SaveChanges();
        }

        using (var context = new FoodDeliveryDbContext(dbContextOptions))
        {
            var repository = new EfRepository<Restaurant, Guid>(context);
            repository.Update(new Restaurant
            {
                Id = id, Name = "Updated pizza test",
            }, new[] { nameof(Restaurant.Name) });

            context.Restaurants.Find(id)!.Name.Should().BeEquivalentTo("Updated pizza test");
        }
    }

    [Fact]
    public void ItUpdatesOnlySelectedProperties()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();
        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        var id = Guid.Parse("00000000-0000-0000-0000-000000000010");
        using (var context = new FoodDeliveryDbContext(dbContextOptions))
        {
            context.Restaurants.Add(new Restaurant { Id = id, Name = "Pizza test" });
            context.SaveChanges();
        }

        using (var context = new FoodDeliveryDbContext(dbContextOptions))
        {
            var repository = new EfRepository<Restaurant, Guid>(context);
            repository.Update(new Restaurant
            {
                Id = id,
                Name = "Updated pizza test",
                DeliveryPrices = new List<Price> { _priceFifty },
            }, new[] { nameof(Restaurant.DeliveryPrices) });

            var foundRestaurant = context.Restaurants.Find(id)!;
            foundRestaurant.Name.Should().BeEquivalentTo("Pizza test");
            foundRestaurant.DeliveryPrices.Should().BeEquivalentTo(new List<Price> { _priceFifty });
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
        Assert.Throws<ArgumentNullException>(() => _repository.Update(null, new List<string>()));
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

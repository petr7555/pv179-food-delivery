using FluentAssertions;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Query;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EntityFramework.Test;

public class QueryTests
{
    private readonly FoodDeliveryDbContext _context;

    private readonly Restaurant _pizzaGiuseppe;
    private readonly Restaurant _pizzaDominos;
    private readonly Restaurant _pizzaHut;
    private readonly Restaurant _k1;
    private readonly Restaurant _jeanPauls;
    private readonly Restaurant _poePoe;

    public QueryTests()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();

        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        _context = new FoodDeliveryDbContext(dbContextOptions);

        var czkCurrency = new Currency { Id = Guid.NewGuid(), Name = "CZK" };
        _context.Currencies.Add(czkCurrency);

        var priceFifty = new Price { Id = Guid.NewGuid(), Amount = 50, CurrencyId = czkCurrency.Id };
        var priceEighty = new Price { Id = Guid.NewGuid(), Amount = 80, CurrencyId = czkCurrency.Id };
        _context.Prices.AddRange(priceFifty, priceEighty);

        _pizzaGiuseppe = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Pizza Guiseppe",
            DeliveryPriceId = priceFifty.Id
        };
        _pizzaDominos = new Restaurant
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Pizza Domino's",
            DeliveryPriceId = priceFifty.Id
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
            Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "POE POE", DeliveryPriceId = priceEighty.Id
        };
        _context.Restaurants.AddRange(_pizzaGiuseppe, _pizzaDominos, _pizzaHut, _k1, _jeanPauls, _poePoe);

        _context.SaveChanges();
    }

    [Fact]
    public async Task ItFiltersPizzaHut()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Where(r => r.Name == "Pizza Hut");
        var result = await query.ExecuteAsync();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaHut,
        });
    }

    [Fact]
    public async Task ItFiltersRestaurantsStartingWithP()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Where(r => r.Name.StartsWith("P"));
        var result = await query.ExecuteAsync();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaGiuseppe,
            _pizzaDominos,
            _pizzaHut,
            _poePoe,
        });
    }

    [Fact]
    public async Task ItFiltersRestaurantsWithSmallerDeliveryPriceThanEighty()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Where(r => r.DeliveryPrice.Amount < 80);
        var result = await query.ExecuteAsync();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaGiuseppe,
            _pizzaDominos,
        });
    }

    [Fact]
    public async Task ItFiltersByMultipleWheres()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Where(r => r.Name.StartsWith("P"))
            .Where(r => r.DeliveryPrice.Amount > 50);
        var result = await query.ExecuteAsync();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaHut,
            _poePoe,
        });
    }

    [Fact]
    public async Task ItOrdersByNameAscending()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.OrderBy(r => r.Name);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _jeanPauls,
            _pizzaDominos,
            _pizzaGiuseppe,
            _pizzaHut,
            _poePoe,
            _k1,
        }, c => c.WithStrictOrdering());
    }

    [Fact]
    public async Task ItOrdersByNameDescending()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.OrderBy(r => r.Name, true);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _k1,
            _poePoe,
            _pizzaHut,
            _pizzaGiuseppe,
            _pizzaDominos,
            _jeanPauls,
        }, c => c.WithStrictOrdering());
    }

    [Fact]
    public async Task ItOrdersByIdDescending()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.OrderBy(r => r.Id, true);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _poePoe,
            _jeanPauls,
            _k1,
            _pizzaHut,
            _pizzaDominos,
            _pizzaGiuseppe,
        }, c => c.WithStrictOrdering());
    }


    [Fact]
    public async Task ItOrdersByTheLastOrderBy()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.OrderBy(r => r.Id, true)
            .OrderBy(r => r.Name);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _jeanPauls,
            _pizzaDominos,
            _pizzaGiuseppe,
            _pizzaHut,
            _poePoe,
            _k1,
        }, c => c.WithStrictOrdering());
    }


    [Fact]
    public async Task ItPagesFirstPageAllRecords()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Page(1, 3);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaGiuseppe,
            _pizzaDominos,
            _pizzaHut,
        });
    }

    [Fact]
    public async Task ItPagesFirstPageSubsetOfRecords()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Page(1, 2);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaGiuseppe,
            _pizzaDominos,
        });
    }

    [Fact]
    public async Task ItPagesSecondPageSubsetOfRecords()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Page(2, 2);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaHut,
            _k1,
        });
    }

    [Fact]
    public async Task ItPagesSecondPageRestOfRecords()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Page(2, 4);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _jeanPauls,
            _poePoe,
        });
    }

    [Fact]
    public async Task ItPagesThirdPageSubsetOfRecords()
    {
        var query = new EfQuery<Restaurant>(_context);
        query.Page(3, 2);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _jeanPauls,
            _poePoe,
        });
    }

    [Fact]
    public async Task ItFiltersOrdersAndPages()
    {
        var query = new EfQuery<Restaurant>(_context);
        query
            .Where(r => r.Name.Contains("Pizza"))
            .OrderBy(r => r.Name)
            .Page(2, 1);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaGiuseppe,
        }, c => c.WithStrictOrdering());
    }

    [Fact]
    public async Task ItAppliesPagingLast()
    {
        var query = new EfQuery<Restaurant>(_context);
        query
            .Page(2, 1)
            .Where(r => r.Name.Contains("Pizza"))
            .OrderBy(r => r.Name);
        var result = (await query.ExecuteAsync()).ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            _pizzaGiuseppe,
        }, c => c.WithStrictOrdering());
    }
}

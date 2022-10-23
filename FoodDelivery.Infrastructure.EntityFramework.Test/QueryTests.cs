using FluentAssertions;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Query;
using FoodDelivery.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Assert = Xunit.Assert;

namespace FoodDelivery.Infrastructure.EntityFramework.Test;

public class QueryTests
{
    private readonly FoodDeliveryDbContext _dbContext;
    private readonly Price price;
    public QueryTests()
    {
        var databaseName = "QueryTests_db_" + DateTime.Now.ToFileTimeUtc();

        var dbContextOptions = new DbContextOptionsBuilder<FoodDeliveryDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        _dbContext = new FoodDeliveryDbContext(dbContextOptions);

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
    public void ItFiltersPizzaHut()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Where(r => r.Name == "Pizza Hut");
        var result = query.Execute();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItFiltersRestaurantsStartingWithP()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Where(r => r.Name.StartsWith("P"));
        var result = query.Execute();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItFiltersRestaurantsWithIdSmallerThanThree()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Where(r => r.Id < 3);
        var result = query.Execute();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItFiltersByMultipleWheres()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Where(r => r.Name.StartsWith("P"))
            .Where(r => r.Id > 2);
        var result = query.Execute();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItOrdersByNameAscending()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.OrderBy(r => r.Name);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 5, Name = "Jean Paul's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 4, Name = "Steak House K1", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItOrdersByNameDescending()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.OrderBy(r => r.Name, true);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 4, Name = "Steak House K1", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 5, Name = "Jean Paul's", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItOrdersByIdDescending()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.OrderBy(r => r.Id);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 5, Name = "Jean Paul's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 4, Name = "Steak House K1", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }


    [Fact]
    public void ItOrdersByTheLastOrderBy()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.OrderBy(r => r.Id, true)
            .OrderBy(r => r.Name);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 5, Name = "Jean Paul's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 4, Name = "Steak House K1", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }


    [Fact]
    public void ItPagesFirstPageAllRecords()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Page(1, 3);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItPagesFirstPageSubsetOfRecords()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Page(1, 2);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 2, Name = "Pizza Domino's", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItPagesSecondPageSubsetOfRecords()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Page(2, 2);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 3, Name = "Pizza Hut", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 4, Name = "Steak House K1", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItPagesSecondPageRestOfRecords()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Page(2, 4);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 5, Name = "Jean Paul's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItPagesThirdPageSubsetOfRecords()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query.Page(3, 2);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 5, Name = "Jean Paul's", DeliveryPriceId = price.Id, DeliveryPrice = price },
            new() { Id = 6, Name = "POE POE", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItFiltersOrdersAndPages()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query
            .Where(r => r.Name.Contains("Pizza"))
            .OrderBy(r => r.Name)
            .Page(2, 1);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }

    [Fact]
    public void ItAppliesPagingLast()
    {
        var query = new EfQuery<Restaurant>(_dbContext);
        query
            .Page(2, 1)
            .Where(r => r.Name.Contains("Pizza"))
            .OrderBy(r => r.Name);
        var result = query.Execute().ToList();

        result.Should().BeEquivalentTo(new List<Restaurant>
        {
            new() { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = price.Id, DeliveryPrice = price },
        });
    }
}
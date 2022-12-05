using FoodDelivery.DAL.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        /**********************
         **   COMPANY INFO   **
         **********************/

        var companyInfo = new CompanyInfo
            { Id = Guid.NewGuid(), CompanyName = "Inkluzívna spoločnosť s.r.o.", Vat = "SK2156O" };

        modelBuilder.Entity<CompanyInfo>().HasData(companyInfo);

        /*****************
         **   ADDRESS   **
         *****************/

        var firstAddress = new Address
        {
            Id = Guid.NewGuid(), FullName = "Jozef Straka", StreetAddress = "SNP 42", City = "Bratislava",
            State = "Slovakia",
            ZipCode = "821 04", PhoneNumber = "+421914123456",
        };
        var secondAddress = new Address
        {
            Id = Guid.NewGuid(), FullName = "Jozef Straka", StreetAddress = "Môťová 69", City = "Zvolen",
            State = "Slovakia",
            ZipCode = "960 01", PhoneNumber = "+421914123456",
        };

        modelBuilder.Entity<Address>().HasData(firstAddress, secondAddress);

        /**************************
         **   CUSTOMER DETAILS   **
         **************************/

        var customerDetails = new CustomerDetails
        {
            Id = Guid.NewGuid(), Email = "jozef.straka@funkcnymail.sk", BillingAddressId = secondAddress.Id,
            DeliveryAddressId = firstAddress.Id,
            CompanyInfoId = companyInfo.Id,
        };

        modelBuilder.Entity<CustomerDetails>().HasData(customerDetails);

        /***************
         **   USERS   **
         ***************/

        var firstUser = new User { Id = Guid.NewGuid(), Username = "admin@example.com" };
        var secondUser = new User { Id = Guid.NewGuid(), Username = "cm@example.com" };
        var thirdUser = new User
            { Id = Guid.NewGuid(), Username = "customer@example.com", CustomerDetailsId = customerDetails.Id };

        modelBuilder.Entity<User>().HasData(firstUser, secondUser, thirdUser);

        /********************
         **   CATEGORIES   **
         ********************/

        var allCategory = new Category { Id = Guid.NewGuid(), Name = "All" };

        var foodCategory = new Category { Id = Guid.NewGuid(), Name = "Food", ParentCategoryId = allCategory.Id };
        var drinkCategory = new Category { Id = Guid.NewGuid(), Name = "Drink", ParentCategoryId = allCategory.Id };

        var alcoholicCategory = new Category
            { Id = Guid.NewGuid(), Name = "Alcoholic", ParentCategoryId = drinkCategory.Id };
        var nonAlcoholicCategory = new Category
            { Id = Guid.NewGuid(), Name = "Non-alcoholic", ParentCategoryId = drinkCategory.Id };

        var pizzaCategory = new Category { Id = Guid.NewGuid(), Name = "Pizza", ParentCategoryId = foodCategory.Id };
        var pastaCategory = new Category { Id = Guid.NewGuid(), Name = "Pasta", ParentCategoryId = foodCategory.Id };
        var saladCategory = new Category { Id = Guid.NewGuid(), Name = "Salad", ParentCategoryId = foodCategory.Id };
        var burgerCategory = new Category { Id = Guid.NewGuid(), Name = "Burger", ParentCategoryId = foodCategory.Id };
        var steakCategory = new Category { Id = Guid.NewGuid(), Name = "Steak", ParentCategoryId = foodCategory.Id };
        var sushiCategory = new Category { Id = Guid.NewGuid(), Name = "Sushi", ParentCategoryId = foodCategory.Id };
        var wineCategory = new Category { Id = Guid.NewGuid(), Name = "Wine", ParentCategoryId = alcoholicCategory.Id };
        var beerCategory = new Category { Id = Guid.NewGuid(), Name = "Beer", ParentCategoryId = alcoholicCategory.Id };
        var whiskeyCategory = new Category
            { Id = Guid.NewGuid(), Name = "Whiskey", ParentCategoryId = alcoholicCategory.Id };
        var softDrinksCategory = new Category
            { Id = Guid.NewGuid(), Name = "Soft Drinks", ParentCategoryId = nonAlcoholicCategory.Id };
        var coffeeCategory = new Category
            { Id = Guid.NewGuid(), Name = "Coffee", ParentCategoryId = nonAlcoholicCategory.Id };
        var teaCategory = new Category
            { Id = Guid.NewGuid(), Name = "Tea", ParentCategoryId = nonAlcoholicCategory.Id };

        modelBuilder.Entity<Category>().HasData(
            allCategory, foodCategory, drinkCategory, alcoholicCategory, nonAlcoholicCategory, pizzaCategory,
            pastaCategory, saladCategory, burgerCategory, steakCategory, sushiCategory, wineCategory,
            beerCategory, whiskeyCategory, softDrinksCategory, coffeeCategory, teaCategory
        );

        /********************
         **   CURRENCIES   **
         ********************/

        var czkCurrency = new Currency { Id = Guid.NewGuid(), Name = "CZK" };

        modelBuilder.Entity<Currency>().HasData(czkCurrency);

        /****************
         **   PRICES   **
         ****************/

        var pizzaPrice = new Price { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id };
        var pizzeriaGiuseppeDeliveryPrice = new Price { Id = Guid.NewGuid(), Amount = 29, CurrencyId = czkCurrency.Id };

        var californiaSalmonEightRollsPrice = new Price
            { Id = Guid.NewGuid(), Amount = 169, CurrencyId = czkCurrency.Id };
        var salmonTunaPrawnEightMakiEachPrice = new Price
            { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id };
        var happySushiDeliveryPrice = new Price { Id = Guid.NewGuid(), Amount = 39, CurrencyId = czkCurrency.Id };

        var barbecueBurgerPrice = new Price { Id = Guid.NewGuid(), Amount = 239, CurrencyId = czkCurrency.Id };
        var chickenBurgerPrice = new Price { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id };
        var royalBurgerPrice = new Price { Id = Guid.NewGuid(), Amount = 269, CurrencyId = czkCurrency.Id };
        var devilBurgerPrice = new Price { Id = Guid.NewGuid(), Amount = 249, CurrencyId = czkCurrency.Id };
        var burgerinoDeliveryPrice = new Price { Id = Guid.NewGuid(), Amount = 25, CurrencyId = czkCurrency.Id };

        modelBuilder.Entity<Price>().HasData(
            pizzaPrice, pizzeriaGiuseppeDeliveryPrice,
            californiaSalmonEightRollsPrice, salmonTunaPrawnEightMakiEachPrice, happySushiDeliveryPrice,
            barbecueBurgerPrice, chickenBurgerPrice, royalBurgerPrice, devilBurgerPrice, burgerinoDeliveryPrice
        );

        /*************************
         **   PAYMENT METHODS   **
         *************************/

        var cardPaymentMethod = new PaymentMethod { Id = Guid.NewGuid(), Type = "Card" };

        modelBuilder.Entity<PaymentMethod>().HasData(cardPaymentMethod);

        /*********************
         **   RESTAURANTS   **
         *********************/

        var pizzeriaGuiseppe = new Restaurant
            { Id = Guid.NewGuid(), Name = "Pizza Guiseppe", DeliveryPriceId = pizzeriaGiuseppeDeliveryPrice.Id };
        var happySushi = new Restaurant
            { Id = Guid.NewGuid(), Name = "Happy Sushi", DeliveryPriceId = happySushiDeliveryPrice.Id };
        var burgerino = new Restaurant
            { Id = Guid.NewGuid(), Name = "Burgerino", DeliveryPriceId = burgerinoDeliveryPrice.Id };

        modelBuilder.Entity<Restaurant>().HasData(
            pizzeriaGuiseppe,
            happySushi,
            burgerino
        );

        /******************
         **   PRODUCTS   **
         ******************/

        var pizzaSalami = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = pizzaCategory.Id,
            Name = "Salami",
            Description = "Tomato salsa, Ham, Spicy salami, Mozzarella",
            RestaurantId = pizzeriaGuiseppe.Id,
            PriceId = pizzaPrice.Id,
        };

        var californiaSalmonEightRolls = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = sushiCategory.Id,
            Name = "California Salmon Eight",
            Description = "California rolls with salmon",
            RestaurantId = happySushi.Id,
            PriceId = californiaSalmonEightRollsPrice.Id,
        };

        var salmonTunaPrawnEightMakiEach = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = sushiCategory.Id,
            Name = "24 Salmon, tuna and prawn maki mix",
            Description = "Salmon, tuna and prawn maki with 8 pieces each.",
            RestaurantId = happySushi.Id,
            PriceId = salmonTunaPrawnEightMakiEachPrice.Id,
        };

        var barbecueBurger = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = burgerCategory.Id,
            Name = "Barbecue Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, onion rings, lettuce, tomato, barbecue sauce, fries",
            RestaurantId = burgerino.Id,
            PriceId = barbecueBurgerPrice.Id,
        };

        var chickenBurger = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = burgerCategory.Id,
            Name = "Chicken Burger",
            Description = "Special homemade bun, 150g chicken, lettuce, tomato, homemade mayo with herbs, fries",
            RestaurantId = burgerino.Id,
            PriceId = chickenBurgerPrice.Id,
        };

        var royalBurger = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = burgerCategory.Id,
            Name = "Royal Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, roasted smoked bacon, egg, caramelized onion, lettuce, tomato, fries",
            RestaurantId = burgerino.Id,
            PriceId = royalBurgerPrice.Id,
        };

        var devilBurger = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = burgerCategory.Id,
            Name = "Devil Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, roasted smoked bacon, red habanero mayo, lettuce, tomato, fries",
            RestaurantId = burgerino.Id,
            PriceId = devilBurgerPrice.Id,
        };

        modelBuilder.Entity<Product>().HasData(
            pizzaSalami,
            californiaSalmonEightRolls, salmonTunaPrawnEightMakiEach,
            barbecueBurger, chickenBurger, royalBurger, devilBurger
        );

        /****************
         **   ORDERS   **
         ****************/

        var pizzaSalamiAndCaliforniaSalmonEightRollsOrder = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            CustomerDetailsId = customerDetails.Id,
            OrderStatus = OrderStatus.Paid,
            // PaymentMethodId = cardPaymentMethod.Id,
        };

        var royalBurgerOrder = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            CustomerDetailsId = customerDetails.Id,
            OrderStatus = OrderStatus.Paid,
            // PaymentMethodId = cardPaymentMethod.Id,
        };

        modelBuilder.Entity<Order>().HasData(pizzaSalamiAndCaliforniaSalmonEightRollsOrder, royalBurgerOrder);

        /*****************************
         **   ORDER-PRODUCT TABLE   **
         *****************************/

        modelBuilder
            .Entity<OrderProduct>()
            .HasData(
                new
                {
                    Id = Guid.NewGuid(), OrderId = pizzaSalamiAndCaliforniaSalmonEightRollsOrder.Id,
                    ProductId = pizzaSalami.Id
                },
                new
                {
                    Id = Guid.NewGuid(), OrderId = pizzaSalamiAndCaliforniaSalmonEightRollsOrder.Id,
                    ProductId = californiaSalmonEightRolls.Id
                },
                new { Id = Guid.NewGuid(), OrderId = royalBurgerOrder.Id, ProductId = royalBurger.Id }
            );

        /*****************
         **   RATINGS   **
         *****************/

        var pizzeriaGiuseppeRating = new Rating
        {
            Id = Guid.NewGuid(),
            RestaurantId = pizzeriaGuiseppe.Id,
            OrderId = pizzaSalamiAndCaliforniaSalmonEightRollsOrder.Id,
            Comment = "Delicious and crusty pizza.",
            CreatedAt = DateTime.UtcNow,
            Stars = 4,
        };

        modelBuilder.Entity<Rating>().HasData(pizzeriaGiuseppeRating);
    }
}

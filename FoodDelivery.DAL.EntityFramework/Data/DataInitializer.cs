using FoodDelivery.DAL.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        /***************
         **   ROLES   **
         ***************/

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Content Manager" },
            new Role { Id = 3, Name = "Customer" }
        );

        /**********************
         **   COMPANY INFO   **
         **********************/

        modelBuilder.Entity<CompanyInfo>().HasData(
            new CompanyInfo { Id = 1, CompanyName = "Inkluzívna spoločnosť s.r.o.", Vat = "SK2156O" },
            new CompanyInfo { Id = 2, CompanyName = "Trio a.s.", Vat = "CZ6373S" }
        );

        /*****************
         **   ADDRESS   **
         *****************/

        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                Id = 1, FullName = "Jozef Straka", StreetAddress = "SNP 42", City = "Bratislava", State = "Slovakia",
                ZipCode = "821 04", PhoneNumber = "+421914123456"
            },
            new Address
            {
                Id = 2, FullName = "Michal Vrbovský", StreetAddress = "Klácelova 4", City = "Brno",
                State = "Czech Republic", ZipCode = "60 200", PhoneNumber = "+420905765934"
            },
            new Address
            {
                Id = 3, FullName = "Bohumil Sedláček", StreetAddress = "Dornych 7", City = "Brno",
                State = "Czech Republic", ZipCode = "60 200", PhoneNumber = "+420911974664"
            },
            new Address
            {
                Id = 4, FullName = "Jozef Straka", StreetAddress = "Môťová 69", City = "Zvolen", State = "Slovakia",
                ZipCode = "960 01", PhoneNumber = "+421914123456"
            }
        );

        /**************************
         **   CUSTOMER DETAILS   **
         **************************/

        modelBuilder.Entity<CustomerDetails>().HasData(
            new CustomerDetails
            {
                Id = 1, Email = "jozef.straka@funkcnymail.sk", BillingAddressId = 4, DeliveryAddressId = 1,
                CompanyInfoId = 1
            },
            new CustomerDetails
            {
                Id = 2, Email = "michal.vrbovsky@funkcnymail.sk", BillingAddressId = 1,
                CompanyInfoId = 1
            },
            new CustomerDetails
            {
                Id = 3, Email = "bohumil.sedlacek@funkcnymail.sk", BillingAddressId = 1,
                CompanyInfoId = 1
            }
        );

        /***************
         **   USERS   **
         ***************/

        var customer = new User
            { Id = 3, Username = "customer", PasswordHash = "customer", Salt = "3", RoleId = 3, CustomerDetailsId = 3 };

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1, Username = "admin", PasswordHash = "admin", Salt = "1", RoleId = 1, CustomerDetailsId = 1
            },
            new User
            {
                Id = 2, Username = "manager", PasswordHash = "manager", Salt = "2", RoleId = 2, CustomerDetailsId = 2
            },
            customer
        );

        /********************
         **   CATEGORIES   **
         ********************/

        var allCategory = new Category { Id = 1, Name = "All" };

        var foodCategory = new Category { Id = 2, Name = "Food", ParentCategoryId = allCategory.Id };
        var drinkCategory = new Category { Id = 3, Name = "Drink", ParentCategoryId = allCategory.Id };

        var alcoholicCategory = new Category { Id = 4, Name = "Alcoholic", ParentCategoryId = drinkCategory.Id };
        var nonAlcoholicCategory = new Category { Id = 5, Name = "Non-alcoholic", ParentCategoryId = drinkCategory.Id };

        var pizzaCategory = new Category { Id = 6, Name = "Pizza", ParentCategoryId = foodCategory.Id };
        var pastaCategory = new Category { Id = 7, Name = "Pasta", ParentCategoryId = foodCategory.Id };
        var saladCategory = new Category { Id = 8, Name = "Salad", ParentCategoryId = foodCategory.Id };
        var burgerCategory = new Category { Id = 9, Name = "Burger", ParentCategoryId = foodCategory.Id };
        var steakCategory = new Category { Id = 10, Name = "Steak", ParentCategoryId = foodCategory.Id };
        var sushiCategory = new Category { Id = 11, Name = "Sushi", ParentCategoryId = foodCategory.Id };
        var wineCategory = new Category { Id = 12, Name = "Wine", ParentCategoryId = alcoholicCategory.Id };
        var beerCategory = new Category { Id = 13, Name = "Beer", ParentCategoryId = alcoholicCategory.Id };
        var whiskeyCategory = new Category { Id = 14, Name = "Whiskey", ParentCategoryId = alcoholicCategory.Id };
        var softDrinksCategory = new Category
            { Id = 15, Name = "Soft Drinks", ParentCategoryId = nonAlcoholicCategory.Id };
        var coffeeCategory = new Category { Id = 16, Name = "Coffee", ParentCategoryId = nonAlcoholicCategory.Id };
        var teaCategory = new Category { Id = 17, Name = "Tea", ParentCategoryId = nonAlcoholicCategory.Id };

        modelBuilder.Entity<Category>().HasData(
            allCategory, foodCategory, drinkCategory, alcoholicCategory, nonAlcoholicCategory, pizzaCategory,
            pastaCategory, saladCategory, burgerCategory, steakCategory, sushiCategory, wineCategory,
            beerCategory, whiskeyCategory, softDrinksCategory, coffeeCategory, teaCategory
        );


        /********************
         **   CURRENCIES   **
         ********************/

        var czkCurrency = new Currency { Id = 1, Name = "CZK" };

        modelBuilder.Entity<Currency>().HasData(czkCurrency);

        /****************
         **   PRICES   **
         ****************/

        var pizzaPrice = new Price { Id = 1, Amount = 199, CurrencyId = czkCurrency.Id };
        var pizzeriaGiuseppeDeliveryPrice = new Price { Id = 2, Amount = 29, CurrencyId = czkCurrency.Id };

        var californiaSalmonEightRollsPrice = new Price { Id = 3, Amount = 169, CurrencyId = czkCurrency.Id };
        var salmonTunaPrawnEightMakiEachPrice = new Price { Id = 4, Amount = 199, CurrencyId = czkCurrency.Id };
        var happySushiDeliveryPrice = new Price { Id = 5, Amount = 39, CurrencyId = czkCurrency.Id };

        var barbecueBurgerPrice = new Price { Id = 6, Amount = 239, CurrencyId = czkCurrency.Id };
        var chickenBurgerPrice = new Price { Id = 7, Amount = 199, CurrencyId = czkCurrency.Id };
        var royalBurgerPrice = new Price { Id = 8, Amount = 269, CurrencyId = czkCurrency.Id };
        var devilBurgerPrice = new Price { Id = 9, Amount = 249, CurrencyId = czkCurrency.Id };
        var burgerinoDeliveryPrice = new Price { Id = 10, Amount = 25, CurrencyId = czkCurrency.Id };

        modelBuilder.Entity<Price>().HasData(
            pizzaPrice, pizzeriaGiuseppeDeliveryPrice,
            californiaSalmonEightRollsPrice, salmonTunaPrawnEightMakiEachPrice, happySushiDeliveryPrice,
            barbecueBurgerPrice, chickenBurgerPrice, royalBurgerPrice, devilBurgerPrice, burgerinoDeliveryPrice
        );

        /*************************
         **   PAYMENT METHODS   **
         *************************/

        var cardPaymentMethod = new PaymentMethod { Id = 1, Type = "Card" };

        modelBuilder.Entity<PaymentMethod>().HasData(cardPaymentMethod);

        /*********************
         **   RESTAURANTS   **
         *********************/

        var pizzeriaGuiseppe = new Restaurant
            { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceId = pizzeriaGiuseppeDeliveryPrice.Id };
        var happySushi = new Restaurant { Id = 2, Name = "Happy Sushi", DeliveryPriceId = happySushiDeliveryPrice.Id };
        var burgerino = new Restaurant { Id = 3, Name = "Burgerino", DeliveryPriceId = burgerinoDeliveryPrice.Id };

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
            Id = 1,
            CategoryId = pizzaCategory.Id,
            Name = "Salami",
            Description = "Tomato salsa, Ham, Spicy salami, Mozzarella",
            RestaurantId = pizzeriaGuiseppe.Id,
            PriceId = pizzaPrice.Id
        };

        var californiaSalmonEightRolls = new Product
        {
            Id = 2,
            CategoryId = sushiCategory.Id,
            Name = "California Salmon Eight",
            Description = "California rolls with salmon",
            RestaurantId = happySushi.Id,
            PriceId = californiaSalmonEightRollsPrice.Id
        };

        var salmonTunaPrawnEightMakiEach = new Product
        {
            Id = 3,
            CategoryId = sushiCategory.Id,
            Name = "24 Salmon, tuna and prawn maki mix",
            Description = "Salmon, tuna and prawn maki with 8 pieces each.",
            RestaurantId = happySushi.Id,
            PriceId = salmonTunaPrawnEightMakiEachPrice.Id
        };

        var barbecueBurger = new Product
        {
            Id = 4,
            CategoryId = burgerCategory.Id,
            Name = "Barbecue Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, onion rings, lettuce, tomato, barbecue sauce, fries",
            RestaurantId = burgerino.Id,
            PriceId = barbecueBurgerPrice.Id
        };

        var chickenBurger = new Product
        {
            Id = 5,
            CategoryId = burgerCategory.Id,
            Name = "Chicken Burger",
            Description = "Special homemade bun, 150g chicken, lettuce, tomato, homemade mayo with herbs, fries",
            RestaurantId = burgerino.Id,
            PriceId = chickenBurgerPrice.Id
        };

        var royalBurger = new Product
        {
            Id = 6,
            CategoryId = burgerCategory.Id,
            Name = "Royal Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, roasted smoked bacon, egg, caramelized onion, lettuce, tomato, fries",
            RestaurantId = burgerino.Id,
            PriceId = royalBurgerPrice.Id
        };

        var devilBurger = new Product
        {
            Id = 7,
            CategoryId = burgerCategory.Id,
            Name = "Devil Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, roasted smoked bacon, red habanero mayo, lettuce, tomato, fries",
            RestaurantId = burgerino.Id,
            PriceId = devilBurgerPrice.Id
        };

        modelBuilder.Entity<Product>().HasData(
            pizzaSalami,
            californiaSalmonEightRolls, salmonTunaPrawnEightMakiEach,
            barbecueBurger, chickenBurger, royalBurger, devilBurger
        );

        /****************
         **   ORDERS   **
         ****************/

        var pizzaSalamiOrder = new Order
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            CustomerId = customer.Id,
            PaymentMethodId = cardPaymentMethod.Id,
        };

        var royalBurgerOrder = new Order
        {
            Id = 2,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            CustomerId = customer.Id,
            PaymentMethodId = cardPaymentMethod.Id
        };

        modelBuilder.Entity<Order>().HasData(pizzaSalamiOrder, royalBurgerOrder);

        /*****************************
         **   ORDER-PRODUCT TABLE   **
         *****************************/

        modelBuilder
            .Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
            .UsingEntity(etb =>
                etb.HasData(new { OrdersId = 1, ProductsId = 1 }, new { OrdersId = 2, ProductsId = 6 }));

        /*****************
         **   RATINGS   **
         *****************/

        var pizzeriaGiuseppeRating = new Rating
        {
            Id = 1,
            RestaurantId = pizzeriaGuiseppe.Id,
            OrderId = pizzaSalamiOrder.Id,
            Comment = "Delicious and crusty pizza.",
            CreatedAt = DateTime.UtcNow,
            Stars = 5
        };

        modelBuilder.Entity<Rating>().HasData(pizzeriaGiuseppeRating);
    }
}

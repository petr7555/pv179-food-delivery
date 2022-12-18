using FoodDelivery.DAL.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EntityFramework.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        /********************
         **   CURRENCIES   **
         ********************/

        var czkCurrency = new Currency { Id = Guid.NewGuid(), Name = "CZK" };
        var eurCurrency = new Currency { Id = Guid.NewGuid(), Name = "EUR" };

        modelBuilder.Entity<Currency>().HasData(czkCurrency, eurCurrency);

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
            SelectedCurrencyId = czkCurrency.Id,
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

        /*************************
         **   PAYMENT METHODS   **
         *************************/

        var cardPaymentMethod = new PaymentMethod { Id = Guid.NewGuid(), Type = "Card" };

        modelBuilder.Entity<PaymentMethod>().HasData(cardPaymentMethod);

        /*********************
         **   RESTAURANTS   **
         *********************/

        var pizzeriaGuiseppe = new Restaurant { Id = Guid.NewGuid(), Name = "Pizza Guiseppe" };
        var happySushi = new Restaurant { Id = Guid.NewGuid(), Name = "Happy Sushi" };
        var burgerino = new Restaurant { Id = Guid.NewGuid(), Name = "Burgerino" };

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
            Name = "Pizza Salami",
            Description = "Tomato salsa, Ham, Spicy salami, Mozzarella",
            ImageUrl = "https://media-cdn.tripadvisor.com/media/photo-s/10/90/53/e8/pizza-salami.jpg",
            RestaurantId = pizzeriaGuiseppe.Id,
        };

        var californiaSalmonEightRolls = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = sushiCategory.Id,
            Name = "California Salmon Eight",
            Description = "California rolls with salmon",
            ImageUrl = "https://www.sushimisanantonio.es/wp-content/uploads/2017/04/california-salmon-2.jpg",
            RestaurantId = happySushi.Id,
        };

        var salmonTunaPrawnEightMakiEach = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = sushiCategory.Id,
            Name = "24 Salmon, tuna and prawn maki mix",
            Description = "Salmon, tuna and prawn maki with 8 pieces each.",
            ImageUrl = "https://makfahealth.com/upload/iblock/48b/48b8e460bedd7e1037f571095409699a.jpg",
            RestaurantId = happySushi.Id,
        };

        var barbecueBurger = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = burgerCategory.Id,
            Name = "Barbecue Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, onion rings, lettuce, tomato, barbecue sauce, fries",
            ImageUrl = "https://recipes.net/wp-content/uploads/2021/10/the-best-grilled-bbq-burger-recipe.jpg",
            RestaurantId = burgerino.Id,
        };

        var chickenBurger = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = burgerCategory.Id,
            Name = "Chicken Burger",
            Description = "Special homemade bun, 150g chicken, lettuce, tomato, homemade mayo with herbs, fries",
            ImageUrl =
                "https://media.istockphoto.com/id/652832752/photo/fried-chicken-burger.jpg?s=612x612&w=0&k=20&c=EendRCleaNpkKOUiOplgStACHh_8IyHYzjbzcByGC_4=",
            RestaurantId = burgerino.Id,
        };

        var royalBurger = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = burgerCategory.Id,
            Name = "Royal Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, roasted smoked bacon, egg, caramelized onion, lettuce, tomato, fries",
            ImageUrl =
                "https://img.freepik.com/premium-photo/royal-burger-with-double-meat-cutlet_127425-327.jpg?w=2000",
            RestaurantId = burgerino.Id,
        };

        var devilBurger = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = burgerCategory.Id,
            Name = "Devil Burger",
            Description =
                "Special homemade bun, 200g beef meat, cheddar, roasted smoked bacon, red habanero mayo, lettuce, tomato, fries",
            ImageUrl = "https://images.hdqwalls.com/wallpapers/bthumb/hot-spicy-burger-ys.jpg",
            RestaurantId = burgerino.Id,
        };

        modelBuilder.Entity<Product>().HasData(
            pizzaSalami,
            californiaSalmonEightRolls, salmonTunaPrawnEightMakiEach,
            barbecueBurger, chickenBurger, royalBurger, devilBurger
        );

        /****************
         **   PRICES   **
         ****************/

        var pizzeriaGiuseppeDeliveryPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 29, CurrencyId = czkCurrency.Id, RestaurantId = pizzeriaGuiseppe.Id };
        var pizzeriaGiuseppeDeliveryPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 1.5, CurrencyId = eurCurrency.Id, RestaurantId = pizzeriaGuiseppe.Id };
        var happySushiDeliveryPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 39, CurrencyId = czkCurrency.Id, RestaurantId = happySushi.Id };
        var happySushiDeliveryPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 1.5, CurrencyId = eurCurrency.Id, RestaurantId = happySushi.Id };
        var burgerinoDeliveryPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 25, CurrencyId = czkCurrency.Id, RestaurantId = burgerino.Id };
        var burgerinoDeliveryPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 1, CurrencyId = eurCurrency.Id, RestaurantId = burgerino.Id };

        var pizzaSalamiPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id, ProductId = pizzaSalami.Id };
        var pizzaSalamiPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, ProductId = pizzaSalami.Id };

        var californiaSalmonEightRollsPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 169, CurrencyId = czkCurrency.Id, ProductId = californiaSalmonEightRolls.Id };
        var californiaSalmonEightRollsPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 7, CurrencyId = eurCurrency.Id, ProductId = californiaSalmonEightRolls.Id };
        var salmonTunaPrawnEightMakiEachPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id, ProductId = salmonTunaPrawnEightMakiEach.Id };
        var salmonTunaPrawnEightMakiEachPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, ProductId = salmonTunaPrawnEightMakiEach.Id };

        var barbecueBurgerPriceCzk = new Price { Id = Guid.NewGuid(), Amount = 239, CurrencyId = czkCurrency.Id, ProductId = barbecueBurger.Id };
        var barbecueBurgerPriceEur = new Price { Id = Guid.NewGuid(), Amount = 10, CurrencyId = eurCurrency.Id, ProductId = barbecueBurger.Id };
        var chickenBurgerPriceCzk = new Price { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id, ProductId = chickenBurger.Id };
        var chickenBurgerPriceEur = new Price { Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, ProductId = chickenBurger.Id };
        var royalBurgerPriceCzk = new Price { Id = Guid.NewGuid(), Amount = 269, CurrencyId = czkCurrency.Id, ProductId = royalBurger.Id };
        var royalBurgerPriceEur = new Price { Id = Guid.NewGuid(), Amount = 11, CurrencyId = eurCurrency.Id, ProductId = royalBurger.Id };
        var devilBurgerPriceCzk = new Price { Id = Guid.NewGuid(), Amount = 249, CurrencyId = czkCurrency.Id, ProductId = devilBurger.Id };
        var devilBurgerPriceEur = new Price { Id = Guid.NewGuid(), Amount = 10, CurrencyId = eurCurrency.Id, ProductId = devilBurger.Id };

        modelBuilder.Entity<Price>().HasData(
            pizzaSalamiPriceCzk, pizzaSalamiPriceEur,
            pizzeriaGiuseppeDeliveryPriceCzk, pizzeriaGiuseppeDeliveryPriceEur,
            californiaSalmonEightRollsPriceCzk, californiaSalmonEightRollsPriceEur,
            salmonTunaPrawnEightMakiEachPriceCzk, salmonTunaPrawnEightMakiEachPriceEur,
            happySushiDeliveryPriceCzk, happySushiDeliveryPriceEur,
            barbecueBurgerPriceCzk, barbecueBurgerPriceEur,
            chickenBurgerPriceCzk, chickenBurgerPriceEur,
            royalBurgerPriceCzk, royalBurgerPriceEur,
            devilBurgerPriceCzk, devilBurgerPriceEur,
            burgerinoDeliveryPriceCzk, burgerinoDeliveryPriceEur
        );

        /****************
         **   ORDERS   **
         ****************/

        var pizzaSalamiAndCaliforniaSalmonEightRollsOrder = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            CustomerDetailsId = customerDetails.Id,
            Status = OrderStatus.Paid,
            // PaymentMethodId = cardPaymentMethod.Id,
        };

        var royalBurgerOrder = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            CustomerDetailsId = customerDetails.Id,
            Status = OrderStatus.Paid,
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
                    ProductId = pizzaSalami.Id,
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

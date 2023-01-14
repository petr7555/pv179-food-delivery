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
            Id = Guid.NewGuid(),
            BillingAddressId = secondAddress.Id,
            DeliveryAddressId = firstAddress.Id,
            CompanyInfoId = companyInfo.Id,
        };

        modelBuilder.Entity<CustomerDetails>().HasData(customerDetails);

        /***********************
         **   USER SETTINGS   **
         ***********************/

        var adminSettings = new UserSettings
        {
            Id = Guid.NewGuid(),
            SelectedCurrencyId = czkCurrency.Id,
        };

        var contentManagerSettings = new UserSettings
        {
            Id = Guid.NewGuid(),
            SelectedCurrencyId = czkCurrency.Id,
        };

        var customerSettings = new UserSettings
        {
            Id = Guid.NewGuid(),
            SelectedCurrencyId = czkCurrency.Id,
        };

        modelBuilder.Entity<UserSettings>().HasData(
            adminSettings,
            contentManagerSettings,
            customerSettings
        );

        /***************
         **   USERS   **
         ***************/

        var admin = new User { Id = Guid.NewGuid(), Email = "admin@example.com", UserSettingsId = adminSettings.Id };
        var contentManager = new User
            { Id = Guid.NewGuid(), Email = "cm@example.com", UserSettingsId = contentManagerSettings.Id };
        var customer = new User
        {
            Id = Guid.NewGuid(), Email = "customer@example.com", CustomerDetailsId = customerDetails.Id,
            UserSettingsId = customerSettings.Id
        };

        modelBuilder.Entity<User>().HasData(admin, contentManager, customer);

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

        var pizzaMargherita = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = pizzaCategory.Id,
            Name = "Pizza Margherita",
            Description = "Tomato salsa, Mozzarella, Fresh basil",
            ImageUrl =
                "https://images.getrecipekit.com/20220211142347-margherita-9920.jpg?aspect_ratio=4:3&quality=90&",
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
            pizzaSalami, pizzaMargherita,
            californiaSalmonEightRolls, salmonTunaPrawnEightMakiEach,
            barbecueBurger, chickenBurger, royalBurger, devilBurger
        );

        /****************
         **   ORDERS   **
         ****************/

        var finalPizzeriaGiuseppeDeliveryPriceCzkId = Guid.NewGuid();
        var finalBurgerinoDeliveryPriceEurId = Guid.NewGuid();

        var pizzaSalamiAndPizzaMargheritaOrder = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            CustomerDetailsId = customerDetails.Id,
            Status = OrderStatus.Paid,
            PaymentMethod = PaymentMethod.Cash,
            FinalCurrencyId = czkCurrency.Id,
            FinalDeliveryPriceId = finalPizzeriaGiuseppeDeliveryPriceCzkId,
        };

        var royalBurgerOrder = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            CustomerDetailsId = customerDetails.Id,
            Status = OrderStatus.Paid,
            PaymentMethod = PaymentMethod.Card,
            FinalCurrencyId = eurCurrency.Id,
            FinalDeliveryPriceId = finalBurgerinoDeliveryPriceEurId,
        };

        modelBuilder.Entity<Order>().HasData(pizzaSalamiAndPizzaMargheritaOrder, royalBurgerOrder);

        /*****************
         **   COUPONS   **
         *****************/

        var expiredCoupon = new Coupon
        {
            Id = Guid.NewGuid(),
            Code = "SALE2020",
            ValidUntil = new DateTime(2020, 12, 31).ToUniversalTime(),
            Status = CouponStatus.Valid,
        };

        var firstUsedCoupon = new Coupon
        {
            Id = Guid.NewGuid(),
            Code = "XYZ132",
            ValidUntil = DateTime.UtcNow.AddDays(30),
            Status = CouponStatus.Used,
            OrderId = pizzaSalamiAndPizzaMargheritaOrder.Id,
        };

        var secondUsedCoupon = new Coupon
        {
            Id = Guid.NewGuid(),
            Code = "XYZ456",
            ValidUntil = DateTime.UtcNow.AddDays(30),
            Status = CouponStatus.Used,
            OrderId = pizzaSalamiAndPizzaMargheritaOrder.Id,
        };

        var firstValidCoupon = new Coupon
        {
            Id = Guid.NewGuid(),
            Code = "ABC123",
            ValidUntil = DateTime.UtcNow.AddDays(30),
            Status = CouponStatus.Valid,
        };

        var secondValidCoupon = new Coupon
        {
            Id = Guid.NewGuid(),
            Code = "DEF456",
            ValidUntil = DateTime.UtcNow.AddDays(30),
            Status = CouponStatus.Valid,
        };

        modelBuilder.Entity<Coupon>().HasData(expiredCoupon, firstUsedCoupon, secondUsedCoupon, firstValidCoupon,
            secondValidCoupon);

        /****************
         **   PRICES   **
         ****************/

        var pizzeriaGiuseppeDeliveryPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 29, CurrencyId = czkCurrency.Id, RestaurantId = pizzeriaGuiseppe.Id };
        var finalPizzeriaGiuseppeDeliveryPriceCzk = new Price
            { Id = finalPizzeriaGiuseppeDeliveryPriceCzkId, Amount = 29, CurrencyId = czkCurrency.Id };
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
        var finalBurgerinoDeliveryPriceEur = new Price
            { Id = finalBurgerinoDeliveryPriceEurId, Amount = 1, CurrencyId = eurCurrency.Id };

        var pizzaSalamiPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id, ProductId = pizzaSalami.Id };
        var finalPizzaSalamiPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id };
        var pizzaSalamiPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, ProductId = pizzaSalami.Id };
        var pizzaMargheritaPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 149, CurrencyId = czkCurrency.Id, ProductId = pizzaMargherita.Id };
        var finalPizzaMargheritaPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 149, CurrencyId = czkCurrency.Id };
        var pizzaMargheritaPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 6, CurrencyId = eurCurrency.Id, ProductId = pizzaMargherita.Id };

        var californiaSalmonEightRollsPriceCzk = new Price
        {
            Id = Guid.NewGuid(), Amount = 169, CurrencyId = czkCurrency.Id, ProductId = californiaSalmonEightRolls.Id
        };
        var californiaSalmonEightRollsPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 7, CurrencyId = eurCurrency.Id, ProductId = californiaSalmonEightRolls.Id };
        var salmonTunaPrawnEightMakiEachPriceCzk = new Price
        {
            Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id, ProductId = salmonTunaPrawnEightMakiEach.Id
        };
        var salmonTunaPrawnEightMakiEachPriceEur = new Price
        {
            Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, ProductId = salmonTunaPrawnEightMakiEach.Id
        };

        var barbecueBurgerPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 239, CurrencyId = czkCurrency.Id, ProductId = barbecueBurger.Id };
        var barbecueBurgerPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 10, CurrencyId = eurCurrency.Id, ProductId = barbecueBurger.Id };
        var chickenBurgerPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 199, CurrencyId = czkCurrency.Id, ProductId = chickenBurger.Id };
        var chickenBurgerPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, ProductId = chickenBurger.Id };
        var royalBurgerPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 269, CurrencyId = czkCurrency.Id, ProductId = royalBurger.Id };
        var royalBurgerPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 11, CurrencyId = eurCurrency.Id, ProductId = royalBurger.Id };
        var finalRoyalBurgerPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 11, CurrencyId = eurCurrency.Id };
        var devilBurgerPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 249, CurrencyId = czkCurrency.Id, ProductId = devilBurger.Id };
        var devilBurgerPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 10, CurrencyId = eurCurrency.Id, ProductId = devilBurger.Id };

        var expiredCouponPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 200, CurrencyId = czkCurrency.Id, CouponId = expiredCoupon.Id };
        var expiredCouponPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, CouponId = expiredCoupon.Id };

        var firstUsedCouponPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 200, CurrencyId = czkCurrency.Id, CouponId = firstUsedCoupon.Id };
        var firstUsedCouponPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, CouponId = firstUsedCoupon.Id };

        var secondUsedCouponPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 50, CurrencyId = czkCurrency.Id, CouponId = secondUsedCoupon.Id };
        var secondUsedCouponPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 2, CurrencyId = eurCurrency.Id, CouponId = secondUsedCoupon.Id };

        var firstValidCouponPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 200, CurrencyId = czkCurrency.Id, CouponId = firstValidCoupon.Id };
        var firstValidCouponPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 8, CurrencyId = eurCurrency.Id, CouponId = firstValidCoupon.Id };
        var secondValidCouponPriceCzk = new Price
            { Id = Guid.NewGuid(), Amount = 100, CurrencyId = czkCurrency.Id, CouponId = secondValidCoupon.Id };
        var secondValidCouponPriceEur = new Price
            { Id = Guid.NewGuid(), Amount = 4, CurrencyId = eurCurrency.Id, CouponId = secondValidCoupon.Id };

        modelBuilder.Entity<Price>().HasData(
            pizzaSalamiPriceCzk, finalPizzaSalamiPriceCzk, pizzaSalamiPriceEur,
            pizzaMargheritaPriceCzk, finalPizzaMargheritaPriceCzk, pizzaMargheritaPriceEur,
            pizzeriaGiuseppeDeliveryPriceCzk, finalPizzeriaGiuseppeDeliveryPriceCzk, pizzeriaGiuseppeDeliveryPriceEur,
            californiaSalmonEightRollsPriceCzk, californiaSalmonEightRollsPriceEur,
            salmonTunaPrawnEightMakiEachPriceCzk, salmonTunaPrawnEightMakiEachPriceEur,
            happySushiDeliveryPriceCzk, happySushiDeliveryPriceEur,
            barbecueBurgerPriceCzk, barbecueBurgerPriceEur,
            chickenBurgerPriceCzk, chickenBurgerPriceEur,
            royalBurgerPriceCzk, royalBurgerPriceEur, finalRoyalBurgerPriceEur,
            devilBurgerPriceCzk, devilBurgerPriceEur,
            burgerinoDeliveryPriceCzk, burgerinoDeliveryPriceEur, finalBurgerinoDeliveryPriceEur,
            expiredCouponPriceCzk, expiredCouponPriceEur,
            firstUsedCouponPriceCzk, firstUsedCouponPriceEur,
            secondUsedCouponPriceCzk, secondUsedCouponPriceEur,
            firstValidCouponPriceCzk, firstValidCouponPriceEur,
            secondValidCouponPriceCzk, secondValidCouponPriceEur
        );

        /*****************************
         **   ORDER-PRODUCT TABLE   **
         *****************************/

        modelBuilder
            .Entity<OrderProduct>()
            .HasData(
                new
                {
                    Id = Guid.NewGuid(),
                    OrderId = pizzaSalamiAndPizzaMargheritaOrder.Id,
                    ProductId = pizzaSalami.Id,
                    Quantity = 1,
                    FinalPriceId = finalPizzaSalamiPriceCzk.Id,
                },
                new
                {
                    Id = Guid.NewGuid(),
                    OrderId = pizzaSalamiAndPizzaMargheritaOrder.Id,
                    ProductId = pizzaMargherita.Id,
                    Quantity = 2,
                    FinalPriceId = finalPizzaMargheritaPriceCzk.Id,
                },
                new
                {
                    Id = Guid.NewGuid(),
                    OrderId = royalBurgerOrder.Id,
                    ProductId = royalBurger.Id,
                    Quantity = 1,
                    FinalPriceId = finalRoyalBurgerPriceEur.Id,
                }
            );

        /*****************
         **   RATINGS   **
         *****************/

        var pizzeriaGiuseppeRating = new Rating
        {
            Id = Guid.NewGuid(),
            RestaurantId = pizzeriaGuiseppe.Id,
            OrderId = pizzaSalamiAndPizzaMargheritaOrder.Id,
            Comment = "Delicious and crusty pizza.",
            CreatedAt = DateTime.UtcNow,
            Stars = 4,
        };

        modelBuilder.Entity<Rating>().HasData(pizzeriaGiuseppeRating);
    }
}

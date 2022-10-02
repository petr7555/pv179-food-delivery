using FoodDeliveryDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryDAL.Data;

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
                Id = 2, Email = "michal.vrbovsky@funkcnymail.sk", BillingAddressId = null, DeliveryAddressId = 1,
                CompanyInfoId = 1
            },
            new CustomerDetails
            {
                Id = 3, Email = "bohumil.sedlacek@funkcnymail.sk", BillingAddressId = null, DeliveryAddressId = 1,
                CompanyInfoId = 1
            }
        );

        /***************
         **   USERS   **
         ***************/

        var customer = new User
            { Id = 3, Username = "customer", Password = "customer", Salt = "3", RoleId = 3, CustomerDetailsId = 3 };

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", Password = "admin", Salt = "1", RoleId = 1, CustomerDetailsId = 1 },
            new User
            {
                Id = 2, Username = "manager", Password = "manager", Salt = "2", RoleId = 2, CustomerDetailsId = 2
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

        modelBuilder.Entity<Price>().HasData(pizzaPrice, pizzeriaGiuseppeDeliveryPrice);

        /*************************
         **   PAYMENT METHODS   **
         *************************/

        var cardPaymentMethod = new PaymentMethod { Id = 1, Type = "Card" };

        modelBuilder.Entity<PaymentMethod>().HasData(cardPaymentMethod);

        /*********************
         **   RESTAURANTS   **
         *********************/

        var pizzeriaGuiseppe = new Restaurant
            { Id = 1, Name = "Pizza Guiseppe", DeliveryPriceID = pizzeriaGiuseppeDeliveryPrice.Id };

        modelBuilder.Entity<Restaurant>().HasData(pizzeriaGuiseppe);

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

        modelBuilder.Entity<Product>().HasData(pizzaSalami);

        /****************
         **   ORDERS   **
         ****************/

        List<Product> orderItems = new List<Product>();
        orderItems.Add(pizzaSalami);

        var pizzaSalamiOrder = new Order
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            CustomerId = customer.Id,
            PaymentMethodId = cardPaymentMethod.Id,
        };

        modelBuilder.Entity<Order>().HasData(pizzaSalamiOrder);

        /*****************************
         **   ORDER-PRODUCT TABLE   **
         *****************************/

        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct { Id = 1, OrderId = 1, ProductId = 1 });

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

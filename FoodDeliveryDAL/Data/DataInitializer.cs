using FoodDeliveryDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryDAL.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name="Admin" },
            new Role { Id = 2, Name="Content Manager" },
            new Role { Id = 3, Name="Customer" }
        );

        modelBuilder.Entity<CompanyInfo>().HasData(
            new CompanyInfo { Id = 1, CompanyName="Inkluzívna spoločnosť s.r.o.", Vat="SK2156O" },
            new CompanyInfo { Id = 2, CompanyName="Trio a.s.", Vat="CZ6373S" }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address { Id = 1, FullName="Jozef Straka", StreetAddress="SNP 42", City="Bratislava", State="Slovakia", ZipCode="821 04", PhoneNumber="+421914123456" },
            new Address { Id = 2, FullName="Michal Vrbovský", StreetAddress="Klácelova 4", City="Brno", State="Czech Republic", ZipCode="60 200", PhoneNumber="+420905765934" },
            new Address { Id = 3, FullName="Bohumil Sedláček", StreetAddress="Dornych 7", City="Brno", State="Czech Republic", ZipCode="60 200", PhoneNumber="+420911974664" },
            new Address { Id = 4, FullName="Jozef Straka", StreetAddress="Môťová 69", City="Zvolen", State="Slovakia", ZipCode="960 01", PhoneNumber="+421914123456" }
        );

        modelBuilder.Entity<CustomerDetails>().HasData(
            new CustomerDetails { Id = 1, Email="jozef.straka@funkcnymail.sk", BillingAddressId=4, DeliveryAddressId=1, CompanyInfoId=1 },
            new CustomerDetails { Id = 2, Email="michal.vrbovsky@funkcnymail.sk", BillingAddressId=null, DeliveryAddressId=1, CompanyInfoId=1 },
            new CustomerDetails { Id = 3, Email="bohumil.sedlacek@funkcnymail.sk", BillingAddressId=null, DeliveryAddressId=1, CompanyInfoId=1 }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", Password="admin", Salt="1", RoleId=1, CustomerDetailsId=1 },
            new User { Id = 2, Username = "manager", Password="manager", Salt="2", RoleId=2, CustomerDetailsId=2 },
            new User { Id = 3, Username = "customer", Password="customer", Salt="3", RoleId=3, CustomerDetailsId=3 }
        );
    }
}

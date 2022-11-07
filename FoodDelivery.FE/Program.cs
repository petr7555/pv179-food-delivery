using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var keyVaultConnectionString = builder.Configuration.GetConnectionString("KeyVaultConnectionString");

var app = builder.Build();

// TODO later add development switch
// if (app.Environment.IsDevelopment())
// {
using (var db = new FoodDeliveryDbContext())
{
    Console.WriteLine("Resetting database...");
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Restaurants.Add(new Restaurant
    {
        Name = "Pizza" + keyVaultConnectionString,
        DeliveryPrice = new Price
        {
            Amount = 1.2f,
            Currency = new Currency { Name = "aaa" },
        }
    });
    db.SaveChanges();
}
// }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

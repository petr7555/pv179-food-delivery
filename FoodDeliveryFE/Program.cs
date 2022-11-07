using FoodDelivery.DAL.EntityFramework.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
System.Diagnostics.Trace.TraceInformation("My message!");
var keyVaultConnectionString = builder.Configuration.GetConnectionString("KeyVaultConnectionString");
System.Diagnostics.Trace.TraceInformation("keyVaultConnectionString: " + keyVaultConnectionString);

var app = builder.Build();

// TODO later add development switch
// if (app.Environment.IsDevelopment())
// {
using (var db = new FoodDeliveryDbContext())
{
    Console.WriteLine("Resetting database...");
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
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

using FoodDelivery.BL.Configs;
using FoodDelivery.DAL.EntityFramework.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// BL dependency injection
builder.Services.AddBlDependencies(builder.Configuration.GetConnectionString("DefaultConnection"));

ConfigureIdentity(builder.Services, builder.Configuration.GetConnectionString("IdentityConnection"));

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretApiKey"];

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var defaultDatabase = scope.ServiceProvider.GetRequiredService<FoodDeliveryDbContext>().Database;
    var identityDatabase = scope.ServiceProvider.GetRequiredService<BasicIdentityDbContext>().Database;
    
    if (app.Environment.IsDevelopment())
    {
        // Comment out if you don't want to delete the database on each run
        await defaultDatabase.EnsureDeletedAsync();
        await identityDatabase.EnsureDeletedAsync();
    }

    await defaultDatabase.EnsureCreatedAsync();
    await identityDatabase.EnsureCreatedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();


static void ConfigureIdentity(IServiceCollection services, string identityConnectionString)
{
    services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
            
            options.Password.RequiredLength = 4;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        })
        .AddEntityFrameworkStores<BasicIdentityDbContext>();

    services.ConfigureApplicationCookie(options =>
    {
        options.LogoutPath = "/Identity/Logout";
        options.LoginPath = "/Identity/Login";
    });
    
    services.AddDbContext<BasicIdentityDbContext>(options => options.UseNpgsql(identityConnectionString));
}

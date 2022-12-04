using AutoMapper;
using FoodDelivery.BL.Facades;
using FoodDelivery.BL.Services.OrderService;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.BL.Services.RestaurantService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;
using FoodDelivery.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.BL.Configs;

public static class BlDependencies
{
    public static IServiceCollection AddBlDependencies(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)));
        services.AddDbContext<DbContext, FoodDeliveryDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRestaurantFacade, RestaurantFacade>();

        services.AddScoped<IProductService, ProductService>();
        
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserFacade, UserFacade>();

        return services;
    }
}

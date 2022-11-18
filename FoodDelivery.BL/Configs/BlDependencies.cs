using AutoMapper;
using FoodDelivery.BL.Facades;
using FoodDelivery.BL.Services;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.Infrastructure.EntityFramework.UnitOfWork;
using FoodDelivery.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.BL.Configs;

public static class BlDependencies
{
    public static IServiceCollection AddBlDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)));
        services.AddScoped<DbContext, FoodDeliveryDbContext>();
        // TODO Same as
        // services.AddDbContext<DbContext, FoodDeliveryDbContext>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        
        services.AddScoped<IDbUtilsService, DbUtilsService>();
        
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRestaurantFacade, RestaurantFacade>();

        return services;
    }
}

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
    public static IServiceCollection AddBlDependencies(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)));
        services.AddDbContext<DbContext, FoodDeliveryDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        services.AddScoped<IDbUtilsService, DbUtilsService>();

        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRestaurantFacade, RestaurantFacade>();

        return services;
    }
}

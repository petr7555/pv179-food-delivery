using AutoMapper;
using FoodDelivery.BL.DTOs.Coupon;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.BL.Facades.OrderFacade;
using FoodDelivery.BL.Facades.ProductFacade;
using FoodDelivery.BL.Facades.RestaurantFacade;
using FoodDelivery.BL.Facades.UserFacade;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CouponService;
using FoodDelivery.BL.Services.CurrencyService;
using FoodDelivery.BL.Services.CustomerDetailsService;
using FoodDelivery.BL.Services.OrderProductService;
using FoodDelivery.BL.Services.OrderService;
using FoodDelivery.BL.Services.ProductService;
using FoodDelivery.BL.Services.RestaurantService;
using FoodDelivery.BL.Services.UserService;
using FoodDelivery.BL.Services.UserSettingsService;
using FoodDelivery.DAL.EntityFramework.Data;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.EntityFramework.Query;
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
        services.AddScoped<IQueryObject<RestaurantGetDto, Restaurant>>(sp =>
            new QueryObject<RestaurantGetDto, Restaurant>(
                sp.GetRequiredService<IMapper>(),
                () => new EfQuery<Restaurant>(sp.GetRequiredService<DbContext>())
            )
        );

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddScoped<IQueryObject<ProductGetDto, Product>>(sp =>
            new QueryObject<ProductGetDto, Product>(
                sp.GetRequiredService<IMapper>(),
                () => new EfQuery<Product>(sp.GetRequiredService<DbContext>())
            )
        );

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<IQueryObject<OrderGetDto, Order>>(sp =>
            new QueryObject<OrderGetDto, Order>(
                sp.GetRequiredService<IMapper>(),
                () => new EfQuery<Order>(sp.GetRequiredService<DbContext>())
            )
        );

        services.AddScoped<IOrderProductService, OrderProductService>();
        services.AddScoped<IQueryObject<OrderProductGetDto, OrderProduct>>(sp =>
            new QueryObject<OrderProductGetDto, OrderProduct>(
                sp.GetRequiredService<IMapper>(),
                () => new EfQuery<OrderProduct>(sp.GetRequiredService<DbContext>())
            )
        );

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IQueryObject<UserGetDto, User>>(sp =>
            new QueryObject<UserGetDto, User>(
                sp.GetRequiredService<IMapper>(),
                () => new EfQuery<User>(sp.GetRequiredService<DbContext>())
            )
        );
        
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IQueryObject<CouponGetDto, Coupon>>(sp =>
            new QueryObject<CouponGetDto, Coupon>(
                sp.GetRequiredService<IMapper>(),
                () => new EfQuery<Coupon>(sp.GetRequiredService<DbContext>())
            )
        );
        
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<ICustomerDetailsService, CustomerDetailsService>();
        services.AddScoped<IUserSettingsService, UserSettingsService>();

        return services;
    }
}

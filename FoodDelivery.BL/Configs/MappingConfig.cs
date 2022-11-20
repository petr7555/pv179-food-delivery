using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Configs;

public static class MappingConfig
{
    public static void ConfigureMapping(IMapperConfigurationExpression config)
    {
        config.AddExpressionMapping();

        config.CreateMap<Restaurant, RestaurantGetDto>().ReverseMap();
        config.CreateMap<Restaurant, RestaurantCreateDto>().ReverseMap();

        config.CreateMap<Product, ProductGetDto>().ReverseMap();

        config.CreateMap<Price, PriceGetDto>().ReverseMap();

        config.CreateMap<Currency, CurrencyGetDto>().ReverseMap();
        
        config.CreateMap<Category, CategoryGetDto>().ReverseMap();
    }
}

using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Test;

public static class TestMappingConfig
{
    public static void ConfigureMapping(IMapperConfigurationExpression config)
    {
        config.AddExpressionMapping();
        config.CreateMap<TestEntity, TestDto>().ReverseMap();
        config.CreateMap<Category, CategoryGetDto>().ReverseMap();
        config.CreateMap<Product, ProductGetDto>().ReverseMap();
        config.CreateMap<Restaurant, RestaurantGetDto>().ReverseMap();
    }
}

using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Configs;

public class MappingConfig
{
    public static void ConfigureMapping(IMapperConfigurationExpression config)
    {
        config.AddExpressionMapping();

        config.CreateMap<Restaurant, RestaurantGetDto>().ReverseMap();
        config.CreateMap<Restaurant, RestaurantCreateDto>().ReverseMap();
        config.CreateMap<Restaurant, RestaurantUpdateDto>().ReverseMap();
    }
}

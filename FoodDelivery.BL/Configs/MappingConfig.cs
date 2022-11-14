using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.DTOs.User;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Configs;

public static class MappingConfig
{
    public static void ConfigureMapping(IMapperConfigurationExpression config)
    {
        config.AddExpressionMapping();

        config.CreateMap<Restaurant, RestaurantGetDto>().ReverseMap();

        config.CreateMap<User, UserGetDto>().ReverseMap();

        config.CreateMap<CustomerDetails, CustomerDetailsUpdateDto>().ReverseMap();
    }
}

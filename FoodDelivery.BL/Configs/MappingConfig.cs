using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FoodDelivery.BL.DTOs.Address;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Currency;
using FoodDelivery.BL.DTOs.CustomerDetails;
using FoodDelivery.BL.DTOs.Order;
using FoodDelivery.BL.DTOs.OrderProduct;
using FoodDelivery.BL.DTOs.Price;
using FoodDelivery.BL.DTOs.Product;
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
        config.CreateMap<Restaurant, RestaurantCreateDto>().ReverseMap();

        config.CreateMap<Product, ProductGetDto>().ReverseMap(); 

        config.CreateMap<Price, PriceGetDto>().ReverseMap();

        config.CreateMap<Currency, CurrencyGetDto>().ReverseMap();

        config.CreateMap<Category, CategoryGetDto>().ReverseMap();

        config.CreateMap<User, UserGetDto>().ReverseMap();
        config.CreateMap<User, UserCreateDto>().ReverseMap();
        config.CreateMap<User, UserUpdateDto>().ReverseMap();

        config.CreateMap<CustomerDetails, CustomerDetailsGetDto>().ReverseMap();
        config.CreateMap<CustomerDetails, CustomerDetailsUpdateDto>().ReverseMap();

        config.CreateMap<Address, AddressGetDto>().ReverseMap();
        config.CreateMap<Address, AddressCreateDto>().ReverseMap();
        config.CreateMap<Address, AddressUpdateDto>().ReverseMap();

        config.CreateMap<Order, OrderGetDto>().ReverseMap();
        config.CreateMap<Order, OrderCreateDto>().ReverseMap();
        config.CreateMap<Order, OrderUpdateDto>().ReverseMap();

        config.CreateMap<OrderProduct, OrderProductGetDto>().ReverseMap();
        config.CreateMap<OrderProduct, OrderProductCreateDto>().ReverseMap();
    }
}

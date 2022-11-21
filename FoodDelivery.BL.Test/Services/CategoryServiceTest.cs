using AutoMapper;
using FluentAssertions;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Product;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Repository;
using FoodDelivery.Infrastructure.UnitOfWork;
using Moq;

namespace FoodDelivery.BL.Test.Services;

public class CategoryServiceTest
{
    private static class TestMappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Category, CategoryGetDto>().ReverseMap();
            config.CreateMap<Product, ProductGetDto>().ReverseMap();
            config.CreateMap<Restaurant, RestaurantGetDto>().ReverseMap();
        }
    }

    private readonly IMapper _mapper;
    private readonly Mock<IRepository<Category, int>> _repositoryMock;
    private readonly CategoryService _service;

    public CategoryServiceTest()
    {
        _mapper = new Mapper(new MapperConfiguration(TestMappingConfig.ConfigureMapping));
        _repositoryMock = new Mock<IRepository<Category, int>>();
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(u => u.CategoryRepository)
            .Returns(_repositoryMock.Object);
        
        _service = new CategoryService(unitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task ItGetsUniqueRestaurantsForCategory()
    {
        var category1 = new Category { Id = 1, Name = "Category 1"};
        var category2 = new Category { Id = 2, Name = "Category 2"};
        
        var restaurant1 = new Restaurant { Id = 1, Name = "Restaurant 1" };
        var restaurant2 = new Restaurant { Id = 2, Name = "Restaurant 2" };
        var restaurant3 = new Restaurant { Id = 2, Name = "Restaurant 3" };

        var product1 = new Product { Id = 1, Restaurant = restaurant1 };
        var product2 = new Product { Id = 2, Restaurant = restaurant2 };
        var product3 = new Product { Id = 3, Restaurant = restaurant1 };
        var product4 = new Product { Id = 4, Restaurant = restaurant3 };

        category1.Products = new List<Product> {product1, product3, product4};
        category2.Products = new List<Product> {product2};

        _repositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Category> { category1, category2 });
        
        var result = await _service.GetRestaurantsForCategory(category1.Id);
        result.Should()
            .BeEquivalentTo(new List<Restaurant> { restaurant1, restaurant3 }.Select(_mapper.Map<RestaurantGetDto>));
    }
}

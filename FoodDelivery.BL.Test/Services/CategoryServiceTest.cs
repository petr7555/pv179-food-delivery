using AutoMapper;
using FluentAssertions;
using FoodDelivery.BL.DTOs.Category;
using FoodDelivery.BL.DTOs.Restaurant;
using FoodDelivery.BL.Services;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.Repository;
using FoodDelivery.Infrastructure.UnitOfWork;
using Moq;

namespace FoodDelivery.BL.Test.Services;

public class CategoryServiceTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IRepository<Category, int>> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CategoryService _service;

    public CategoryServiceTest()
    {
        _mapper = new Mapper(new MapperConfiguration(TestMappingConfig.ConfigureMapping));
        _repositoryMock = new Mock<IRepository<Category, int>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(u => u.CategoryRepository)
            .Returns(_repositoryMock.Object);
        
        _service = new CategoryService(_unitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task ItGetsAllRestaurants()
    {
        var category1 = new Category { Id = 1, Name = "Category 1"};
        var category2 = new Category { Id = 2, Name = "Category 2"};
        
        var restaurant1 = new Restaurant { Id = 1, Name = "Restaurant 1" };
        var restaurant2 = new Restaurant { Id = 2, Name = "Restaurant 2" };

        var product1 = new Product { Id = 1, Category = category1, Restaurant = restaurant1 };
        var product2 = new Product { Id = 2, Category = category2, Restaurant = restaurant2 };

        category1.Products = new() {product1};
        category2.Products = new() {product2};

        _repositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Category> { category1, category2 });
        
        var result = await _service.GetRestaurantsForCategory(_mapper.Map<CategoryGetDto>(category1));
        result.Should()
            .BeEquivalentTo((new List<Restaurant> { restaurant1 }).Select(r => _mapper.Map<RestaurantGetDto>(r)));
    }
    
    [Fact]
    public async Task ItGetsAllRestaurantsOther()
    {
        var category1 = new Category { Id = 1, Name = "Category 1"};
        var category2 = new Category { Id = 2, Name = "Category 2"};
        
        var restaurant1 = new Restaurant { Id = 1, Name = "Restaurant 1" };
        var restaurant2 = new Restaurant { Id = 2, Name = "Restaurant 2" };

        var product1 = new Product { Id = 1, Category = category1, Restaurant = restaurant1 };
        var product2 = new Product { Id = 2, Category = category2, Restaurant = restaurant2 };

        category1.Products = new() {product1};
        category2.Products = new() {product2};

        _repositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Category> { category1, category2 });
        
        var result = await _service.GetRestaurantsForCategory(_mapper.Map<CategoryGetDto>(category2));
        result.Should()
            .BeEquivalentTo((new List<Restaurant> { restaurant2 }).Select(r => _mapper.Map<RestaurantGetDto>(r)));
    }
}

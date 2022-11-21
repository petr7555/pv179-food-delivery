using AutoMapper;
using FluentAssertions;
using FoodDelivery.BL.Services;
using FoodDelivery.Infrastructure.Repository;
using Moq;

namespace FoodDelivery.BL.Test.Services;

public class CrudServiceTest
{
    private class TestService : CrudService<TestEntity, int, TestDto, TestDto, TestDto>
    {
        public TestService(IRepository<TestEntity, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }

    private static class TestMappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<TestEntity, TestDto>().ReverseMap();
        }
    }

    private readonly IMapper _mapper;
    private readonly Mock<IRepository<TestEntity, int>> _repositoryMock;

    public CrudServiceTest()
    {
        _mapper = new Mapper(new MapperConfiguration(TestMappingConfig.ConfigureMapping));
        _repositoryMock = new Mock<IRepository<TestEntity, int>>();
    }

    [Fact]
    public async Task ItGetsEntityById()
    {
        var entity = new TestEntity { Id = 1, Name = "Test Entity 1" };

        _repositoryMock.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(entity);

        var service = new TestService(_repositoryMock.Object, _mapper);

        var result = await service.GetByIdAsync(1);

        result.Should().BeEquivalentTo(_mapper.Map<TestDto>(entity));
    }

    [Fact]
    public async Task ItGetsAllEntities()
    {
        var entity1 = new TestEntity { Id = 1, Name = "Test Entity 1" };
        var entity2 = new TestEntity { Id = 2, Name = "Test Entity 2" };
        var allEntities = new List<TestEntity> { entity1, entity2 };

        _repositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(allEntities);

        var service = new TestService(_repositoryMock.Object, _mapper);

        var result = await service.GetAllAsync();

        result.Should()
            .BeEquivalentTo(allEntities.Select(r => _mapper.Map<TestDto>(r)));
    }

    [Fact]
    public void ItCreatesNewEntity()
    {
        var entity = new TestEntity { Id = 1, Name = "Test Entity" };

        var service = new TestService(_repositoryMock.Object, _mapper);

        service.Create(_mapper.Map<TestDto>(entity));

        _repositoryMock.Verify(x => x.Create(
            It.Is<TestEntity>(e => e.Id == entity.Id && e.Name == entity.Name)), Times.Once());
    }

    [Fact]
    public void ItUpdatesEntity()
    {
        var entity = new TestEntity { Id = 1, Name = "Test Entity" };

        var service = new TestService(_repositoryMock.Object, _mapper);

        service.Update(_mapper.Map<TestDto>(entity));

        _repositoryMock.Verify(x => x.Update(
            It.Is<TestEntity>(e => e.Id == entity.Id && e.Name == entity.Name)), Times.Once());
    }

    [Fact]
    public void ItDeletesEntity()
    {
        const int id = 1;

        var service = new TestService(_repositoryMock.Object, _mapper);

        service.Delete(id);

        _repositoryMock.Verify(x => x.Delete(id), Times.Once());
    }
}

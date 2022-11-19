
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FluentAssertions;
using FoodDelivery.BL.Services;
using FoodDelivery.Infrastructure.Repository;
using Telerik.JustMock;

namespace FoodDelivery.BL.Test;

public class CrudServiceTest
{
    private readonly TestService _service;
    private readonly Mapper _mapper;
    private readonly TestEntity _entity1;
    private readonly TestEntity _entity2;
    private readonly List<TestEntity> _allEntities;
    private List<TestEntity> _mockDb;
    
    public CrudServiceTest()
    {
        _mapper = new Mapper(new MapperConfiguration(TestMappingConfig.ConfigureMapping));
        _entity1 = new TestEntity { Id = 1, Name = "Test Entity 1" };
        _entity2 = new TestEntity { Id = 2, Name = "Test Entity 2" };
        _allEntities = new List<TestEntity>{ _entity1, _entity2 };
        _mockDb = new List<TestEntity>();
        
        var repository = Mock.Create<IRepository<TestEntity, int>>();
        Mock.Arrange(() => repository.GetByIdAsync(1))
            .ReturnsAsync(() => _entity1);
        Mock.Arrange(() => repository.GetByIdAsync(2))
            .ReturnsAsync(() => _entity2);
        Mock.Arrange(() => repository.GetAllAsync())
            .ReturnsAsync(() => _allEntities);
        Mock.Arrange(() => repository.Create(Arg.IsAny<TestEntity>()))
            .DoInstead((TestEntity r) => _mockDb.Add(r));
        Mock.Arrange(() => repository.Update(Arg.IsAny<TestEntity>()))
            .DoInstead((TestEntity r) =>
            {
                var i = _mockDb.FindIndex(rr => r.Id == rr.Id);
                _mockDb[i] = r;
            });
        Mock.Arrange(() => repository.Delete(Arg.IsAny<int>()))
            .DoInstead((int i) => _mockDb.RemoveAll(r => r.Id == i));

        _service = new TestService(repository, _mapper);
    }
    
    [Fact]
    public void ItGetsByIdAsync()
    {
        var testEntity1 = _service.GetByIdAsync(1).Result;
        testEntity1.Should().BeEquivalentTo(_mapper.Map<TestDto>(_entity1));

        var testEntity2 = _service.GetByIdAsync(2).Result;
        testEntity2.Should().BeEquivalentTo(_mapper.Map<TestDto>(_entity2));
    }

    [Fact]
    public void ItGetsAllAsync()
    {
        var allEntities = _service.GetAllAsync().Result;
        allEntities.Should()
            .BeEquivalentTo(_allEntities.Select(r => _mapper.Map<TestDto>(r)));
    }

    [Fact]
    public void ItCreates()
    {
        var testEntity = new TestEntity { Id = 1, Name = "Test Entity" };
        _service.Create(_mapper.Map<TestDto>(testEntity));
        Assert.Contains(_mockDb,
            e => e.Id == testEntity.Id
                 && e.Name == testEntity.Name); 
        _mockDb.Clear();
    }
    
    [Fact]
    public void ItUpdates()
    {
        var testEntity = new TestEntity { Id = 1, Name = "Test Entity" };
        _mockDb.Add(testEntity);
        var updatedEntity = new TestEntity { Id = 1, Name = "Test Entity with updated name" };
        _service.Update(_mapper.Map<TestDto>(updatedEntity));
        Assert.Contains(_mockDb,
            e => e.Id == updatedEntity.Id
                 && e.Name == updatedEntity.Name); 
        _mockDb.Clear();
    }
    
    [Fact]
    public void ItDeletes()
    {
        var testEntity = new TestEntity { Id = 1, Name = "Test Entity" };
        _mockDb.Add(testEntity);
        _service.Delete(testEntity.Id);
        Assert.DoesNotContain(_mockDb, e => e.Id == testEntity.Id);
    }

    class TestService : CrudService<TestEntity, int, TestDto, TestDto, TestDto>
    {
        public TestService(IRepository<TestEntity, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
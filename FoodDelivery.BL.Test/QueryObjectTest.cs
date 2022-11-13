using AutoMapper;
using FluentAssertions;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.Infrastructure.Query;

namespace FoodDelivery.BL.Test;

public class QueryObjectTest
{
    private readonly Mapper _mapper;
    private readonly QueryObject<TestDto,TestEntity> _queryObject;

    private readonly TestEntity _entityZ;
    private readonly TestEntity _entityA;
    private readonly TestEntity _oneOfEntities;
    private readonly TestEntity _anotherOne;
    private readonly List<TestEntity> _allEntities;

    public QueryObjectTest()
    {
        _entityZ = new() { Id = 1, Name = "Entity Z" };
        _entityA = new() { Id = 2, Name = "Entity A" };
        _oneOfEntities = new() { Id = 3, Name = "One of test entities" };
        _anotherOne = new() { Id = 4, Name = "Another one" };
        _allEntities = new List<TestEntity>
        {
            _entityZ,
            _entityA,
            _oneOfEntities,
            _anotherOne
        };
        
        var query = new TestQuery(_allEntities.AsQueryable());

        _mapper = new Mapper(new MapperConfiguration(TestMappingConfig.ConfigureMapping));
        _queryObject = new QueryObject<TestDto, TestEntity>
        (
            _mapper, 
            query
        );
    }

    [Fact]
    public void ItExecutesContainsEntity()
    {
        var expectedResult = new List<TestEntity>
        {
            _entityA,
            _entityZ
        };
        
        var expectedResultDesc = new List<TestEntity>
        {
            _entityZ,
            _entityA
        };
        
        var dto = new QueryDto<TestDto>()
            .Where(e => e.Name.Contains("Entity"))
            .OrderBy(e => e.Name);
        var dtoDesc = new QueryDto<TestDto>()
            .Where(e => e.Name.Contains("Entity"));
        
        var result = _queryObject.Execute(dto);
        result.Should()
            .BeEquivalentTo(expectedResult.Select(e => _mapper.Map<TestDto>(e)));

        var resultDesc = _queryObject.Execute(dtoDesc);
        resultDesc.Should()
            .BeEquivalentTo(expectedResultDesc.Select(e => _mapper.Map<TestDto>(e)));
    }

    [Fact]
    public void ItExecutesOrderBy()
    {
        var expectedResult = new List<TestEntity>
        {
            _anotherOne,
            _entityA,
            _entityZ,
            _oneOfEntities
        };

        var dtoOrderByName = new QueryDto<TestDto>()
            .OrderBy(e => e.Name);
        var dtoOrderById = new QueryDto<TestDto>()
            .OrderBy(e => e.Id);
        
        var resultByName = _queryObject.Execute(dtoOrderByName);
        resultByName.Should()
            .BeEquivalentTo(expectedResult.Select(e => _mapper.Map<TestDto>(e)));

        var resultById = _queryObject.Execute(dtoOrderById);
        resultById.Should()
            .BeEquivalentTo(_allEntities.Select(e => _mapper.Map<TestDto>(e)));
    }

    [Fact]
    public void ItExecutesEmptyResult()
    {
        var dto = new QueryDto<TestDto>()
            .Where(e => false);
        
        var resultByName = _queryObject.Execute(dto);
        resultByName.Should()
            .BeEquivalentTo(new List<TestDto>());
    }

    [Fact]
    public void ItExecutesWithPaging()
    {
        var dto1 = new QueryDto<TestDto>()
            .Page(1, 2);
        
        var result1 = _queryObject.Execute(dto1);
        result1.Should()
            .BeEquivalentTo(
                (new List<TestEntity>
                {
                    _entityZ,
                    _entityA
                }).Select(e => _mapper.Map<TestDto>(e))
            );
    }

    [Fact]
    public void ItExecutesWithPaging2()
    {
        var dto2 = new QueryDto<TestDto>()
            .Page(2, 2);
        
        var result2 = _queryObject.Execute(dto2);
        result2.Should()
            .BeEquivalentTo
            (
                (new List<TestEntity>
                {
                    _oneOfEntities,
                    _anotherOne
                }).Select(e => _mapper.Map<TestDto>(e))
            );
    }
    
    [Fact]
    public void ItExecutesWithPaging3()
    {
        var dto3 = new QueryDto<TestDto>()
            .Page(2, 3);
        
        var result3 = _queryObject.Execute(dto3);
        result3.Should()
            .BeEquivalentTo
            (
                (new List<TestEntity>
                {
                    _anotherOne
                }).Select(e => _mapper.Map<TestDto>(e))
            );
    }
    
    [Fact]
    public void ItExecutesWithPaging4()
    {
        var dto4 = new QueryDto<TestDto>()
            .Page(2, 5);
        
        var result4 = _queryObject.Execute(dto4);
        result4.Should()
            .BeEquivalentTo
            (
                (new List<TestEntity>()).Select(e => _mapper.Map<TestDto>(e))
            );
    }

    class TestQuery : Query<TestEntity>
    {
        private IQueryable<TestEntity> _allEntities;
        
        public TestQuery(IQueryable<TestEntity> allEntities)
        {
            _allEntities = allEntities;
        }

        public override IEnumerable<TestEntity> Execute()
        {
            _allEntities = ApplyWhere(_allEntities);
            _allEntities = ApplyOrderBy(_allEntities);
            _allEntities = ApplyPaging(_allEntities);

            return _allEntities.ToList();
        }

        private IQueryable<TestEntity> ApplyWhere(IQueryable<TestEntity> query)
        {
            return WherePredicates.Aggregate(query, (current, expr) => current.Where(expr));
        }

        private IQueryable<TestEntity> ApplyOrderBy(IQueryable<TestEntity> query)
        {
            if (!OrderByConfig.HasValue)
            {
                return query;
            }

            var (keySelector, descending) = OrderByConfig.Value;
            return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }

        private IQueryable<TestEntity> ApplyPaging(IQueryable<TestEntity> query)
        {
            if (!PageConfig.HasValue)
            {
                return query;
            }

            var (pageToFetch, pageSize) = PageConfig.Value;
            return query.Skip((pageToFetch - 1) * pageSize).Take(pageSize);
        }
    }
}
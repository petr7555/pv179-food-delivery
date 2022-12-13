using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FluentAssertions;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.Infrastructure.Query;

namespace FoodDelivery.BL.Test.QueryObject;

public class QueryObjectTest
{
    private class TestQuery : Query<TestEntity>
    {
        private IQueryable<TestEntity> _allEntities;

        public TestQuery(IQueryable<TestEntity> allEntities)
        {
            _allEntities = allEntities;
        }

        public override Task<IEnumerable<TestEntity>> ExecuteAsync()
        {
            _allEntities = ApplyWhere(_allEntities);
            _allEntities = ApplyOrderBy(_allEntities);
            _allEntities = ApplyPaging(_allEntities);

            return Task.FromResult<IEnumerable<TestEntity>>(_allEntities.ToList());
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

    private static class TestMappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.AddExpressionMapping();
            config.CreateMap<TestEntity, TestDto>().ReverseMap();
        }
    }

    private readonly IMapper _mapper;
    private readonly QueryObject<TestDto, TestEntity> _queryObject;

    private readonly TestEntity _entityZ;
    private readonly TestEntity _entityA;
    private readonly TestEntity _oneOfEntities;
    private readonly TestEntity _anotherOne;

    public QueryObjectTest()
    {
        _entityA = new TestEntity { Id = 1, Name = "Entity A" };
        _entityZ = new TestEntity { Id = 2, Name = "Entity Z" };
        _oneOfEntities = new TestEntity { Id = 3, Name = "One of test entities" };
        _anotherOne = new TestEntity { Id = 4, Name = "Another one" };

        var allEntities = new List<TestEntity>
        {
            _entityA,
            _entityZ,
            _oneOfEntities,
            _anotherOne,
        };

        _mapper = new Mapper(new MapperConfiguration(TestMappingConfig.ConfigureMapping));
        _queryObject = new QueryObject<TestDto, TestEntity>(_mapper, () => new TestQuery(allEntities.AsQueryable()));
    }

    [Fact]
    public async Task ItReturnsObjectsContainingEntity()
    {
        var dto = new QueryDto<TestDto>()
            .Where(e => e.Name.Contains("Entity"));

        var result = await _queryObject.ExecuteAsync(dto);

        result.Should()
            .BeEquivalentTo(new List<TestEntity>
            {
                _entityA,
                _entityZ,
            }.Select(e => _mapper.Map<TestDto>(e)));
    }

    [Fact]
    public async Task ItReturnsObjectsContainingEntityOrderedByNameDescending()
    {
        var dto = new QueryDto<TestDto>()
            .Where(e => e.Name.Contains("Entity"))
            .OrderBy(e => e.Name, true);

        var result = await _queryObject.ExecuteAsync(dto);

        result.Should()
            .BeEquivalentTo(new List<TestEntity>
                {
                    _entityZ,
                    _entityA,
                }.Select(e => _mapper.Map<TestDto>(e)),
                c => c.WithStrictOrdering());
    }


    [Fact]
    public async Task ItOrdersByNameAscending()
    {
        var dto = new QueryDto<TestDto>()
            .OrderBy(e => e.Name);

        var result = await _queryObject.ExecuteAsync(dto);

        result.Should()
            .BeEquivalentTo(new List<TestEntity>
                {
                    _anotherOne,
                    _entityA,
                    _entityZ,
                    _oneOfEntities,
                }.Select(e => _mapper.Map<TestDto>(e)),
                c => c.WithStrictOrdering());
    }

    [Fact]
    public async Task ItOrdersByIdAscending()
    {
        var dto = new QueryDto<TestDto>()
            .OrderBy(e => e.Id);

        var result = await _queryObject.ExecuteAsync(dto);

        result.Should()
            .BeEquivalentTo(new List<TestEntity>
                {
                    _entityA,
                    _entityZ,
                    _oneOfEntities,
                    _anotherOne,
                }.Select(e => _mapper.Map<TestDto>(e)),
                c => c.WithStrictOrdering());
    }

    [Fact]
    public async Task ItExecutesEmptyResult()
    {
        var dto = new QueryDto<TestDto>()
            .Where(e => false);

        var resultByName = await _queryObject.ExecuteAsync(dto);
        resultByName.Should()
            .BeEquivalentTo(new List<TestDto>());
    }

    [Fact]
    public async Task ItExecutesWithPaging()
    {
        var dto = new QueryDto<TestDto>()
            .Page(1, 2);

        var result = await _queryObject.ExecuteAsync(dto);
        result.Should()
            .BeEquivalentTo(
                new List<TestEntity>
                {
                    _entityZ,
                    _entityA,
                }.Select(e => _mapper.Map<TestDto>(e))
            );
    }

    [Fact]
    public async Task ItExecutesWithPaging2()
    {
        var dto = new QueryDto<TestDto>()
            .Page(2, 2);

        var result = await _queryObject.ExecuteAsync(dto);
        result.Should()
            .BeEquivalentTo
            (
                new List<TestEntity>
                {
                    _oneOfEntities,
                    _anotherOne,
                }.Select(e => _mapper.Map<TestDto>(e))
            );
    }

    [Fact]
    public async Task ItExecutesWithPaging3()
    {
        var dto = new QueryDto<TestDto>()
            .Page(2, 3);

        var result = await _queryObject.ExecuteAsync(dto);
        result.Should()
            .BeEquivalentTo
            (
                new List<TestEntity>
                {
                    _anotherOne,
                }.Select(e => _mapper.Map<TestDto>(e))
            );
    }

    [Fact]
    public async Task ItExecutesWithPaging4()
    {
        var dto = new QueryDto<TestDto>()
            .Page(2, 5);

        var result = await _queryObject.ExecuteAsync(dto);
        result.Should()
            .BeEquivalentTo(
                new List<TestEntity>().Select(e => _mapper.Map<TestDto>(e))
            );
    }
}

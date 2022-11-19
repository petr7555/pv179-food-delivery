using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;

namespace FoodDelivery.BL.Test;

public static class TestMappingConfig
{
    public static void ConfigureMapping(IMapperConfigurationExpression config)
    {
        config.AddExpressionMapping();
        config.CreateMap<TestEntity, TestDto>().ReverseMap();
    }
}

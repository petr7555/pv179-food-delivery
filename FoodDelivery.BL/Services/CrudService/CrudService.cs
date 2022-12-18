using AutoMapper;
using FoodDelivery.Infrastructure.Repository;

namespace FoodDelivery.BL.Services.CrudService;

public class
    CrudService<TEntity, TKey, TGetDto, TCreateDto, TUpdateDto> :
        ICrudService<TEntity, TKey, TGetDto, TCreateDto, TUpdateDto>
    where TEntity : class where TKey : struct
{
    private readonly IRepository<TEntity, TKey> _repository;
    protected readonly IMapper Mapper;

    protected CrudService(IRepository<TEntity, TKey> repository, IMapper mapper)
    {
        _repository = repository;
        Mapper = mapper;
    }

    public async Task<TGetDto?> GetByIdAsync(TKey id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return Mapper.Map<TGetDto>(entity);
    }

    public async Task<IEnumerable<TGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return Mapper.Map<IEnumerable<TGetDto>>(entities);
    }

    public void Create(TCreateDto dto)
    {
        var entity = Mapper.Map<TEntity>(dto);
        _repository.Create(entity);
    }

    public void Update(TUpdateDto dto, IEnumerable<string> propertiesToUpdate)
    {
        var entity = Mapper.Map<TEntity>(dto);
        _repository.Update(entity, propertiesToUpdate);
    }

    public void Delete(TKey id)
    {
        _repository.Delete(id);
    }
}

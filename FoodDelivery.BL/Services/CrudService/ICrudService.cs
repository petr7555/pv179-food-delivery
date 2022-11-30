namespace FoodDelivery.BL.Services.CrudService;

public interface ICrudService<TEntity, TKey, TGetDto, TCreateDto, TUpdateDto> where TEntity : class where TKey : struct
{
    public Task<TGetDto?> GetByIdAsync(TKey id);

    public Task<IEnumerable<TGetDto>> GetAllAsync();

    public void Create(TCreateDto dto);

    public void Update(TUpdateDto dto);

    public void Delete(TKey id);
}

namespace Domain.Interfaces.Repositories.BaseRepository.Crud;

public interface IBaseReadRepository<TValue, TValueId>
{
    Task<TValue?> GetByIdAsync(TValueId id);
    Task<TValue?> GetAllAsync();
    Task<IQueryable<TValue>> GetQueryable();
}
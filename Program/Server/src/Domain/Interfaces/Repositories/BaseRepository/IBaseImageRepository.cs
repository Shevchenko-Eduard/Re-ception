namespace Domain.Interfaces.Repositories.BaseRepository;

public interface IBaseImageRepository<TValue, TValueId>
{
    Task CreateAsync(TValue entity, Stream stream);
    Task DeleteAsync(TValueId id);
    Task UpdateAsync(TValue entity, Stream stream);
    Task<Stream> ReadAsync(TValueId id);
}
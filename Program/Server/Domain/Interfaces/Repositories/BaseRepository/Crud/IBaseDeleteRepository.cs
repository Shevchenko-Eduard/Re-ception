namespace Domain.Interfaces.Repositories.BaseRepository.Crud;

public interface IBaseDeleteRepository<TValue, TValueId>
{
	Task DeleteAsync(TValueId id);
}
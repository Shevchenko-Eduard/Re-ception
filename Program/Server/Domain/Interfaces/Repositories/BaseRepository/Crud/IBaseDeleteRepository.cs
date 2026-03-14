namespace Domain.Interfaces.Repositories.BaseRepository.Crud;

public interface IBaseDeleteRepository<T, TId>
{
	Task DeleteAsync(TId id);
}
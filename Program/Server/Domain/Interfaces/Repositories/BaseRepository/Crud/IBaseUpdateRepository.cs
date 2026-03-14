namespace Domain.Interfaces.Repositories.BaseRepository.Crud;

public interface IBaseUpdateRepository<T>
{
	Task UpdateAsync(T entity);
}
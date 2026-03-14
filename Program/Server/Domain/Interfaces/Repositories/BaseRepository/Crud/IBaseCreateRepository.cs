namespace Domain.Interfaces.Repositories.BaseRepository.Crud;

public interface IBaseCreateRepository<T>
{
    Task AddAsync(T entity);
}
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories.BaseRepository.Crud;

public interface IBaseReadRepository<T, TId>
{
    Task<T?> GetByIdAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> specification);
}
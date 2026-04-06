using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories.BaseRepository.Crud;

public interface IBaseReadRepository<TValue, TValueId>
{
    Task<TValue?> GetByIdAsync(TValueId id);
    Task<IEnumerable<TValue>> GetAllAsync();
    Task<IEnumerable<TValue>?> FindAsync(Expression<Func<TValue, bool>> specification);
    Task<TValue?> FirstAsync(Expression<Func<TValue, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<TValue, bool>> predicate);
    Task<int> CountAsync(Expression<Func<TValue, bool>> predicate);
}
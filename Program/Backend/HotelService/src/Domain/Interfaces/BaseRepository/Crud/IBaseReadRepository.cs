using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories.BaseRepository.Crud;

public interface IBaseReadRepository<TValue, TValueId, TSpecification>
{
    Task<TValue?> GetByIdAsync(TValueId id);
    Task<IEnumerable<TValue>?> FindAsync(TSpecification specification);
    Task<TValue?> FirstAsync(TSpecification predicate);
    Task<bool> ExistsAsync(TSpecification predicate);
    Task<int> CountAsync(TSpecification predicate);
}
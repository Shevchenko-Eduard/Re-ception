using Domain.Interfaces;

namespace Application.Interfaces;

public interface IUnitOfWork
{
    IClock Clock { get; }
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
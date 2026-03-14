using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.BaseRepository;

public interface IBaseCrudRepository<T, TId> :
    IBaseCreateRepository<T>,
    IBaseReadRepository<T, TId>,
    IBaseUpdateRepository<T>,
    IBaseDeleteRepository<T, TId>
{

}
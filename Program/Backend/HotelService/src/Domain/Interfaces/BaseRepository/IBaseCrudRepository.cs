using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.BaseRepository;

public interface IBaseCrudRepository<TValue, TValueId, TSpecification> :
    IBaseCreateRepository<TValue>,
    IBaseReadRepository<TValue, TValueId, TSpecification>,
    IBaseUpdateRepository<TValue>,
    IBaseDeleteRepository<TValueId>
{

}
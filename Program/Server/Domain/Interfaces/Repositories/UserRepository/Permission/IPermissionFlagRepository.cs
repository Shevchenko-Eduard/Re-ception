using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.UserRepository.Permission;

public interface IPermissionFlagRepository : IBaseStatusObjectRepository<PermissionFlag>
{
    
}
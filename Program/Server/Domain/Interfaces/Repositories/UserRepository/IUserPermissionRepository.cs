using Domain.Entity.User;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.UserRepository;

public interface IUserPermissionRepository :
    IBaseCreateRepository<UserPermission>,
    IBaseReadRepository<UserPermission, ulong>,
    IBaseDeleteRepository<UserPermission, ulong>
{

}
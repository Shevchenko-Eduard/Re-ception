using Domain.Interfaces.Repositories.BaseRepository;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.UserRepository.Permission;

public interface IPermissionRepository : IBaseEnumObjectAbstract<Entity.User.Permission.Permission>, IBaseCreateRepository<Entity.User.Permission.Permission>, IBaseDeleteRepository<Entity.User.Permission.Permission, byte>
{
    Task<IEnumerable<Entity.User.Permission.Permission>> GetPermissionsByRolesAsync(IEnumerable<ushort> rolesId);
}
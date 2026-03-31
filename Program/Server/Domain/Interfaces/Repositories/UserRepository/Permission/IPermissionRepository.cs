using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.UserRepository.Permission;

public interface IPermissionRepository : IBaseEnumObjectAbstract<Entity.User.Permission.Permission>
{
    Task<IEnumerable<Entity.User.Permission.Permission>> GetPermissionsByRolesAsync(IEnumerable<ushort> rolesId);
}
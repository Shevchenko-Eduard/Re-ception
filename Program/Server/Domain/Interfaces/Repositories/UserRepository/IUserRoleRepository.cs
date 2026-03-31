using Domain.Entity.User;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.UserRepository;

public interface IUserRoleRepository :
    IBaseCreateRepository<UserRole>,
    IBaseReadRepository<UserRole, ulong>,
    IBaseDeleteRepository<UserRole, ulong>
{
    Task<IEnumerable<ushort>> GetRolesIdByUserAsync(Guid userId);
}
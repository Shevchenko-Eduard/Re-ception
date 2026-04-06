using System.Linq.Expressions;
using Domain.Interfaces.Repositories.UserRepository.Permission;

namespace Infrastructure.EfRepository.UserRepository.Permission;

public abstract class PermissionRepository : IPermissionRepository
{
    public Task AddAsync(Domain.Entity.User.Permission.Permission entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(byte id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entity.User.Permission.Permission>> FindAsync(Expression<Func<Domain.Entity.User.Permission.Permission, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entity.User.Permission.Permission>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entity.User.Permission.Permission?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entity.User.Permission.Permission>> GetPermissionsByRolesAsync(IEnumerable<ushort> rolesId)
    {
        throw new NotImplementedException();
    }
}

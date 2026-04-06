using System.Linq.Expressions;
using Domain.Entity.User;
using Domain.Interfaces.Repositories.UserRepository;

namespace Infrastructure.EfRepository.UserRepository;

public abstract class UserPermissionRepository : IUserPermissionRepository
{
    public Task AddAsync(UserPermission entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserPermission>> FindAsync(Expression<Func<UserPermission, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserPermission>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserPermission?> GetByIdAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entity.User.Permission.Permission>> GetPermissionsByUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}

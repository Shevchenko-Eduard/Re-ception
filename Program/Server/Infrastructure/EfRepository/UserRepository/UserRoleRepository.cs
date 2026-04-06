using System.Linq.Expressions;
using Domain.Entity.User;
using Domain.Interfaces.Repositories.UserRepository;

namespace Infrastructure.EfRepository.UserRepository;

public abstract class UserRoleRepository : IUserRoleRepository
{
    public Task AddAsync(UserRole entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<UserRole, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<UserRole, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserRole>?> FindAsync(Expression<Func<UserRole, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<UserRole?> FirstAsync(Expression<Func<UserRole, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserRole>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserRole?> GetByIdAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ushort>> GetRolesIdByUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}

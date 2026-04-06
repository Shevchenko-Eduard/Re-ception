using System.Linq.Expressions;
using Domain.Entity.User.Role;
using Domain.Interfaces.Repositories.UserRepository.Role;

namespace Infrastructure.EfRepository.UserRepository.Role;

public abstract class RoleRepository : IRoleRepository
{
    public Task AddAsync(Domain.Entity.User.Role.Role entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entity.User.Role.Role>> FindAsync(Expression<Func<Domain.Entity.User.Role.Role, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entity.User.Role.Role>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entity.User.Role.Role?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Entity.User.Role.Role entity)
    {
        throw new NotImplementedException();
    }
}

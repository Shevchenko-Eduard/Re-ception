using System.Linq.Expressions;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository.Permission;

namespace Infrastructure.EfRepository.UserRepository.Permission;

public abstract class PermissionActionRepository : IPermissionActionRepository
{
    public Task<IEnumerable<PermissionFlag>> FindAsync(Expression<Func<PermissionFlag, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PermissionFlag>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PermissionFlag?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PermissionFlag?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}

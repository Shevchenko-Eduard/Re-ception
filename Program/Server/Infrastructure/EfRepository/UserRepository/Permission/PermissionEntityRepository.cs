using System.Linq.Expressions;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.UserRepository.Permission;

namespace Infrastructure.EfRepository.UserRepository.Permission;

public abstract class PermissionEntityRepository : IPermissionEntityRepository
{
    public Task<int> CountAsync(Expression<Func<PermissionEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<PermissionEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PermissionEntity>?> FindAsync(Expression<Func<PermissionEntity, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<PermissionEntity?> FirstAsync(Expression<Func<PermissionEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PermissionEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PermissionEntity?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PermissionEntity?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}

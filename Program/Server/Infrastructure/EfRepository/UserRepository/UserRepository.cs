using System.Linq.Expressions;
using Domain.Entity.User;
using Domain.Interfaces.Repositories.UserRepository;

namespace Infrastructure.EfRepository.UserRepository;

public abstract class UserRepository : IUserRepository
{
    public Task AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<User, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EmailExist(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>?> FindAsync(Expression<Func<User, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<User?> FirstAsync(Expression<Func<User, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}

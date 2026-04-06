using System.Linq.Expressions;
using Domain.Entity.User;
using Domain.Interfaces.Repositories.UserRepository;

namespace Infrastructure.EfRepository.UserRepository;

public abstract class UserGenderRepository : IUserGenderRepository
{
    public Task<int> CountAsync(Expression<Func<UserGender, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<UserGender, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserGender>?> FindAsync(Expression<Func<UserGender, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<UserGender?> FirstAsync(Expression<Func<UserGender, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserGender>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserGender?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserGender?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}

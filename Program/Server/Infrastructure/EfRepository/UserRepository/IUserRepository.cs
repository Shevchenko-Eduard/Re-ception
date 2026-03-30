using Domain.Entity.User;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.UserRepository;

public interface IUserRepository : IBaseCrudRepository<User, Guid>
{
    Task<bool> EmailExist(string email);
}
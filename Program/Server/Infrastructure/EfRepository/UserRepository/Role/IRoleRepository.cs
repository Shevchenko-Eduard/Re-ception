using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.UserRepository.Role;

public interface IRoleRepository : IBaseCrudRepository<Entity.User.Role.Role, int>
{

}
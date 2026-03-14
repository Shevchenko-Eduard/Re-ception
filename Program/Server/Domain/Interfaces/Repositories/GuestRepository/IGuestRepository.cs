using Domain.Entity.Guest;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.GuestRepository;

public interface IGuestRepository : IBaseCrudRepository<Guest, Guid>
{

}
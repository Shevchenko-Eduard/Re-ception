using Domain.Entity.Room;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.RoomRepository;

public interface IRoomTagRepository : IBaseCrudRepository<RoomTag, ushort>
{
    
}
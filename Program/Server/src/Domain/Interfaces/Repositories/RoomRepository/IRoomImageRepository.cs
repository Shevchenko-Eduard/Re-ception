using Domain.Entity.Room;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.RoomRepository;

public interface IRoomImageRepository : IBaseImageRepository<RoomImage, int>
{

}
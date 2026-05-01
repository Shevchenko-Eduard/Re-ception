using Domain.Entity.Room;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.RoomRepository;

public interface IRoomRoomTagRepository : 
    IBaseCreateRepository<RoomRoomTag>, 
    IBaseDeleteRepository<int>,
    IBaseReadRepository<RoomRoomTag, int>
{

}
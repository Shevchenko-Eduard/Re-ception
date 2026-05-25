using Domain.Entity.Room;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.RoomRepository;

public interface IRoomRepository : IBaseCrudRepository<Room, int>
{
    Task<decimal> GetPricePerDay(int id);
    Task LoadRoomStatusAsync(Room room);
}
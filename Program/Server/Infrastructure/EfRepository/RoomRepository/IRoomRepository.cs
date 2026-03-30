using System.Linq.Expressions;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomRepository : IRoomRepository
{
    public Task AddAsync(Room entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ushort id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Room>> FindAsync(Expression<Func<Room, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Room>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Room?> GetByIdAsync(ushort id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Room entity)
    {
        throw new NotImplementedException();
    }
}
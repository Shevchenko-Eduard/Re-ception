using System.Linq.Expressions;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomTagRepository : IRoomTagRepository
{
    public Task AddAsync(RoomTag entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ushort id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoomTag>> FindAsync(Expression<Func<RoomTag, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoomTag>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RoomTag?> GetByIdAsync(ushort id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(RoomTag entity)
    {
        throw new NotImplementedException();
    }
}
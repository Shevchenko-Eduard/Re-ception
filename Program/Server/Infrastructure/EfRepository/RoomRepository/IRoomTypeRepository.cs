using System.Linq.Expressions;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomTypeRepository : IRoomTypeRepository
{
    public Task AddAsync(RoomType entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ushort id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoomType>> FindAsync(Expression<Func<RoomType, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoomType>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RoomType?> GetByIdAsync(ushort id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(RoomType entity)
    {
        throw new NotImplementedException();
    }
}
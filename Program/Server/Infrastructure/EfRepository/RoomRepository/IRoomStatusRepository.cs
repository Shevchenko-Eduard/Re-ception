using System.Linq.Expressions;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomStatusRepository : IRoomStatusRepository
{
    public Task<int> CountAsync(Expression<Func<RoomStatus, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<RoomStatus, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoomStatus>?> FindAsync(Expression<Func<RoomStatus, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<RoomStatus?> FirstAsync(Expression<Func<RoomStatus, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoomStatus>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RoomStatus?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RoomStatus?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
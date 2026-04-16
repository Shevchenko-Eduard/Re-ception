using System.Linq.Expressions;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomStatusRepository(ProgramContext context) : IRoomStatusRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task<RoomStatus?> GetByIdAsync(int id)
    {
        return await _context.RoomStatuses.FirstOrDefaultAsync(rs => rs.Id == id);
    }

    public async Task<IEnumerable<RoomStatus>> GetAllAsync()
    {
        return await _context.RoomStatuses.ToListAsync();
    }

    public async Task<IEnumerable<RoomStatus>?> FindAsync(Expression<Func<RoomStatus, bool>> specification)
    {
        return await _context.RoomStatuses.Where(specification).ToListAsync();
    }

    public async Task<RoomStatus?> FirstAsync(Expression<Func<RoomStatus, bool>> predicate)
    {
        return await _context.RoomStatuses.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<RoomStatus, bool>> predicate)
    {
        return await _context.RoomStatuses.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<RoomStatus, bool>> predicate)
    {
        return await _context.RoomStatuses.CountAsync(predicate);
    }

    public async Task<RoomStatus?> GetByNameAsync(string name)
    {
        return await _context.RoomStatuses.FirstOrDefaultAsync(rs => rs.Name == name);
    }
}
using Domain.Entity.Room;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomRepository(ProgramContext context) : IRoomRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(Room entity)
    {
        await _context.Rooms.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<Room, bool>> predicate)
    {
        return await _context.Rooms.CountAsync(predicate);
    }

    public async Task DeleteAsync(ushort id)
    {
        await _context.Rooms.Where(r => r.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Room, bool>> predicate)
    {
        return await _context.Rooms.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Room>?> FindAsync(Expression<Func<Room, bool>> specification)
    {
        return await _context.Rooms.Where(specification).ToListAsync();
    }

    public async Task<Room?> FirstAsync(Expression<Func<Room, bool>> predicate)
    {
        return await _context.Rooms.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(ushort id)
    {
        return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task UpdateAsync(Room entity)
    {
        _context.Rooms.Update(entity);
    }
}
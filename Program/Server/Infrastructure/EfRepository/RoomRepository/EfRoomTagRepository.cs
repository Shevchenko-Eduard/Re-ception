using Domain.Entity.Room;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomTagRepository(ProgramContext context) : IRoomTagRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(RoomTag entity)
    {
        await _context.RoomTags.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<RoomTag, bool>> predicate)
    {
        return await _context.RoomTags.CountAsync(predicate);
    }

    public async Task DeleteAsync(ushort id)
    {
        await _context.RoomTags.Where(rt => rt.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<RoomTag, bool>> predicate)
    {
        return await _context.RoomTags.AnyAsync(predicate);
    }

    public async Task<IEnumerable<RoomTag>?> FindAsync(Expression<Func<RoomTag, bool>> specification)
    {
        return await _context.RoomTags.Where(specification).ToListAsync();
    }

    public async Task<RoomTag?> FirstAsync(Expression<Func<RoomTag, bool>> predicate)
    {
        return await _context.RoomTags.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<RoomTag>> GetAllAsync()
    {
        return await _context.RoomTags.ToListAsync();
    }

    public async Task<RoomTag?> GetByIdAsync(ushort id)
    {
        return await _context.RoomTags.FirstOrDefaultAsync(rt => rt.Id == id);
    }

    public async Task UpdateAsync(RoomTag entity)
    {
        _context.RoomTags.Update(entity);
    }
}
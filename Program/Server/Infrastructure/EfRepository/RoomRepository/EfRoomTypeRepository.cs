using System.Linq.Expressions;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomTypeRepository(ProgramContext context) : IRoomTypeRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(RoomType entity)
    {
        await _context.RoomTypes.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<RoomType, bool>> predicate)
    {
        return await _context.RoomTypes.CountAsync(predicate);
    }

    public async Task DeleteAsync(ushort id)
    {
        await _context.RoomTypes.Where(rt => rt.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<RoomType, bool>> predicate)
    {
        return await _context.RoomTypes.AnyAsync(predicate);
    }

    public async Task<IEnumerable<RoomType>?> FindAsync(Expression<Func<RoomType, bool>> specification)
    {
        return await _context.RoomTypes.Where(specification).ToListAsync();
    }

    public async Task<RoomType?> FirstAsync(Expression<Func<RoomType, bool>> predicate)
    {
        return await _context.RoomTypes.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<RoomType>> GetAllAsync()
    {
        return await _context.RoomTypes.ToListAsync();
    }

    public async Task<RoomType?> GetByIdAsync(ushort id)
    {
        return await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.Id == id);
    }

    public async Task UpdateAsync(RoomType entity)
    {
        _context.RoomTypes.Update(entity);
    }
}
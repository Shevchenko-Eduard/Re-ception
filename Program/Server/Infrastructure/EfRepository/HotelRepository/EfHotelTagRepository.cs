using System.Linq.Expressions;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelTagRepository(ProgramContext context) : IHotelTagRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(HotelTag entity)
    {
        await _context.HotelTags.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<HotelTag, bool>> predicate)
    {
        return await _context.HotelTags.CountAsync(predicate);
    }

    public async Task DeleteAsync(ushort id)
    {
        await _context.HotelTags.Where(ht => ht.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<HotelTag, bool>> predicate)
    {
        return await _context.HotelTags.AnyAsync(predicate);
    }

    public async Task<IEnumerable<HotelTag>?> FindAsync(Expression<Func<HotelTag, bool>> specification)
    {
        return await _context.HotelTags.Where(specification).ToListAsync();
    }

    public async Task<HotelTag?> FirstAsync(Expression<Func<HotelTag, bool>> predicate)
    {
        return await _context.HotelTags.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<HotelTag>> GetAllAsync()
    {
        return await _context.HotelTags.ToListAsync();
    }

    public async Task<HotelTag?> GetByIdAsync(ushort id)
    {
        return await _context.HotelTags.FirstOrDefaultAsync(ht => ht.Id == id);
    }

    public async Task UpdateAsync(HotelTag entity)
    {
        _context.HotelTags.Update(entity);
    }
}
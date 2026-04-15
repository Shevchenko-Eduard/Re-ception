using Domain.Entity.Hotel;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.HotelRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelRepository(ProgramContext context) : IHotelRepository
{
    private readonly ProgramContext _context = context;
    public async Task AddAsync(Hotel entity)
    {
        await _context.Hotels.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<Hotel, bool>> predicate)
    {
        return await _context.Hotels.CountAsync(predicate);
    }

    public async Task DeleteAsync(byte id)
    {
        await _context.Hotels.Where(h => h.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Hotel, bool>> predicate)
    {
        return await _context.Hotels.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Hotel>?> FindAsync(Expression<Func<Hotel, bool>> specification)
    {
        return await _context.Hotels.Where(specification).ToListAsync();
    }

    public async Task<Hotel?> FirstAsync(Expression<Func<Hotel, bool>> predicate)
    {
        return await _context.Hotels.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
        return await _context.Hotels.ToListAsync();
    }

    public async Task<Hotel?> GetByIdAsync(byte id)
    {
        return await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task UpdateAsync(Hotel entity)
    {
        _context.Hotels.Update(entity);
    }
}
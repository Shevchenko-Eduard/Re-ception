using System.Linq.Expressions;
using Domain.Entity.Guest;
using Domain.Interfaces.Repositories.GuestRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.GuestRepository;

public class EfGuestRepository(ProgramContext context) : IGuestRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(Guest entity)
    {
        await _context.Guests.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<Guest, bool>> predicate)
    {
        return await _context.Guests.CountAsync(predicate);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Guests.Where(g => g.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Guest, bool>> predicate)
    {
        return await _context.Guests.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Guest>?> FindAsync(Expression<Func<Guest, bool>> specification)
    {
        return await _context.Guests.Where(specification).ToListAsync();
    }

    public async Task<Guest?> FirstAsync(Expression<Func<Guest, bool>> predicate)
    {
        return await _context.Guests.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Guest>> GetAllAsync()
    {
        return await _context.Guests.ToListAsync();
    }

    public async Task<Guest?> GetByIdAsync(Guid id)
    {
        return await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task UpdateAsync(Guest entity)
    {
        _context.Guests.Update(entity);
    }
}
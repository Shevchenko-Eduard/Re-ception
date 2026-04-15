using Domain.Entity.Reservation;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.ReservationRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.ReservationRepository;

public class EfReservationRepository(ProgramContext context) : IReservationRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task AddAsync(Reservation entity)
    {
        await _context.Reservations.AddAsync(entity);
    }

    public async Task<int> CountAsync(Expression<Func<Reservation, bool>> predicate)
    {
        return await _context.Reservations.CountAsync(predicate);
    }

    public async Task DeleteAsync(ulong id)
    {
        await _context.Reservations.Where(r => r.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<Reservation, bool>> predicate)
    {
        return await _context.Reservations.AnyAsync(predicate);
    }

    public async Task<IEnumerable<Reservation>?> FindAsync(Expression<Func<Reservation, bool>> specification)
    {
        return await _context.Reservations.Where(specification).ToListAsync();
    }

    public async Task<Reservation?> FirstAsync(Expression<Func<Reservation, bool>> predicate)
    {
        return await _context.Reservations.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(ulong id)
    {
        return await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task UpdateAsync(Reservation entity)
    {
        _context.Reservations.Update(entity);
    }
}
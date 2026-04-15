using Domain.Entity.Reservation;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.ReservationRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.ReservationRepository;

public class EfReservationStatusRepository(ProgramContext context) : IReservationStatusRepository
{
    private readonly ProgramContext _context = context;
    
    public async Task<ReservationStatus?> GetByIdAsync(int id)
    {
        return await _context.ReservationStatuses.FirstOrDefaultAsync(rs => rs.Id == id);
    }

    public async Task<IEnumerable<ReservationStatus>> GetAllAsync()
    {
        return await _context.ReservationStatuses.ToListAsync();
    }

    public async Task<IEnumerable<ReservationStatus>?> FindAsync(Expression<Func<ReservationStatus, bool>> specification)
    {
        return await _context.ReservationStatuses.Where(specification).ToListAsync();
    }

    public async Task<ReservationStatus?> FirstAsync(Expression<Func<ReservationStatus, bool>> predicate)
    {
        return await _context.ReservationStatuses.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<ReservationStatus, bool>> predicate)
    {
        return await _context.ReservationStatuses.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<ReservationStatus, bool>> predicate)
    {
        return await _context.ReservationStatuses.CountAsync(predicate);
    }

    public async Task<ReservationStatus?> GetByNameAsync(string name)
    {
        return await _context.ReservationStatuses.FirstOrDefaultAsync(rs => rs.Name == name);
    }
}
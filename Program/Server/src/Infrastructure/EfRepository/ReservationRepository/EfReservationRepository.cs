using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.ReservationRepository;

public class EfReservationRepository(ProgramContext context) : IReservationRepository
{
    private readonly ProgramContext _context = context;

    public async Task AddAsync(Reservation entity) => await _context.Reservations.AddAsync(entity);

    public async Task DeleteAsync(int id) => await _context.Reservations.Where(r => r.Id == id).ExecuteDeleteAsync();

    public async Task<Reservation?> GetByIdAsync(int id) => await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

    public IQueryable<Reservation> GetQueryable() => _context.Reservations.AsQueryable();

    public async Task UpdateAsync(Reservation entity) => _context.Reservations.Update(entity);
}
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.ReservationRepository;

public class EfReservationStatusRepository(ProgramContext context) : IReservationStatusRepository
{
    private readonly ProgramContext _context = context;

    public async Task<ReservationStatus?> GetByIdAsync(int id) => await _context.ReservationStatuses.FirstOrDefaultAsync(rs => rs.Id == id);

    public async Task<ReservationStatus?> GetByNameAsync(string name) => await _context.ReservationStatuses.FirstOrDefaultAsync(rs => rs.Name == name);

}
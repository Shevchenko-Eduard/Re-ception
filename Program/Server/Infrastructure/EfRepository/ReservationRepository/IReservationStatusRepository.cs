using System.Linq.Expressions;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Infrastructure.EfRepository.ReservationRepository;

public class EfReservationStatusRepository : IReservationStatusRepository
{
    public Task<IEnumerable<ReservationStatus>> FindAsync(Expression<Func<ReservationStatus, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReservationStatus>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ReservationStatus?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ReservationStatus?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
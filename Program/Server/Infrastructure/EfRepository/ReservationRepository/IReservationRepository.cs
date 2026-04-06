using System.Linq.Expressions;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Infrastructure.EfRepository.ReservationRepository;

public class EfReservationRepository : IReservationRepository
{
    public Task AddAsync(Reservation entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<Reservation, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<Reservation, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>?> FindAsync(Expression<Func<Reservation, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<Reservation?> FirstAsync(Expression<Func<Reservation, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Reservation?> GetByIdAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Reservation entity)
    {
        throw new NotImplementedException();
    }
}
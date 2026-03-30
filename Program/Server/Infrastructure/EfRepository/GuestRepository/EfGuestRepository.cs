using System.Linq.Expressions;
using Domain.Entity.Guest;
using Domain.Interfaces.Repositories.GuestRepository;

namespace Infrastructure.EfRepository.GuestRepository;

public class EfGuestRepository : IGuestRepository
{
    public Task AddAsync(Guest entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Guest>> FindAsync(Expression<Func<Guest, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<Guest> FirstAsync(Expression<Func<Guest, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Guest>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Guest?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guest entity)
    {
        throw new NotImplementedException();
    }
}
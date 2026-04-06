using Domain.Entity.Hotel;
using System.Linq.Expressions;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelRepository : IHotelRepository
{
    public Task AddAsync(Hotel entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<Hotel, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(byte id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<Hotel, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Hotel>?> FindAsync(Expression<Func<Hotel, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<Hotel?> FirstAsync(Expression<Func<Hotel, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Hotel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Hotel?> GetByIdAsync(byte id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Hotel entity)
    {
        throw new NotImplementedException();
    }
}
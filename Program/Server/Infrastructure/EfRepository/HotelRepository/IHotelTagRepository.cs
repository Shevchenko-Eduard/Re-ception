using System.Linq.Expressions;
using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelTagRepository : IHotelTagRepository
{
    public Task AddAsync(HotelTag entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<HotelTag, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ushort id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<HotelTag, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<HotelTag>?> FindAsync(Expression<Func<HotelTag, bool>> specification)
    {
        throw new NotImplementedException();
    }

    public Task<HotelTag?> FirstAsync(Expression<Func<HotelTag, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<HotelTag>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<HotelTag?> GetByIdAsync(ushort id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(HotelTag entity)
    {
        throw new NotImplementedException();
    }
}
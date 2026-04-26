using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository;
using Domain.Specification;

namespace Domain.Interfaces.Repositories.HotelRepository;

public interface IHotelRepository : IBaseCrudRepository<Hotel, int, HotelSpec>
{
    
}
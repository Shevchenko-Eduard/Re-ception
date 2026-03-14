using Domain.Interfaces.Repositories.BaseRepository;
using Domain.Entity.Hotel;

namespace Domain.Interfaces.Repositories.HotelRepository;

public interface IHotelRepository : IBaseCrudRepository<Hotel, byte>
{
    
}
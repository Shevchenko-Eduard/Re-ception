using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.HotelRepository;

public interface IHotelTagRepository : IBaseCrudRepository<HotelTag, ushort>
{
    
}
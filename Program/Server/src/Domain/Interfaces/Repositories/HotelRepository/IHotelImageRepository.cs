using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.HotelRepository;

public interface IHotelImageRepository: IBaseCrudRepository<HotelImage, int>
{
    
}
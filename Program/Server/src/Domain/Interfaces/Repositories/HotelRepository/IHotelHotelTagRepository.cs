using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.Repositories.HotelRepository;

public interface IHotelHotelTagRepository: 
    IBaseCreateRepository<HotelHotelTag>, 
    IBaseDeleteRepository<int>,
    IBaseReadRepository<HotelHotelTag, int>
{
    
}
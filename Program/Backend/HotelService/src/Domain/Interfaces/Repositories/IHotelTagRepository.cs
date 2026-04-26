using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository.Crud;

namespace Domain.Interfaces.HotelRepository;

public interface IHotelTagRepository: 
    IBaseCreateRepository<HotelTag>, 
    IBaseDeleteRepository<int>,
    IBaseReadRepository<HotelTag, int>
{
    
}
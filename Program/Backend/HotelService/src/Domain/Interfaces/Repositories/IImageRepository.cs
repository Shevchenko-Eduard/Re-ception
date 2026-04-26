using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository;
using Domain.Specification;

namespace Domain.Interfaces.HotelRepository;

public interface IImageRepository: IBaseCrudRepository<Image, int, ImageSpec>
{
    
}
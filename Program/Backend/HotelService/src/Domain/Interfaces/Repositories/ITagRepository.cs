using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.HotelRepository;

public interface ITagRepository : IBaseCrudRepository<Tag, int>
{
    
}
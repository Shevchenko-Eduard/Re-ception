using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository;
using Domain.Specification;

namespace Domain.Interfaces.Repositories.HotelRepository;

public interface ITagRepository : IBaseCrudRepository<Tag, int, TagSpec>
{
    
}
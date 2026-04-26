using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.HotelRepository;

public interface IImageRepository: IBaseCrudRepository<Image, int>
{
    Task<Image> GetImageAsync(int id);
}
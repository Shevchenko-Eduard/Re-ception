using Domain.Entity.Hotel;
using Domain.Interfaces.Repositories.HotelRepository;
using Infrastructure.Database;
using Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.HotelRepository;

public class EfHotelImageRepository(
    ProgramContext context,
    IS3HotelImageRepository s3HotelImageRepository) : IHotelImageRepository
{
    private readonly ProgramContext _context = context;
    private readonly IS3HotelImageRepository _s3HotelImageRepository = s3HotelImageRepository;
    public async Task CreateAsync(HotelImage entity, Stream stream)
    {
        await _context.HotelImages.AddAsync(entity);
        await _s3HotelImageRepository.UploadAsync(stream, entity.ImageKey.ToString());
    }

    public async Task DeleteAsync(int id)
    {
        HotelImage hotelImage = await GetValueByIdAsync(id) ?? throw new Exception();
        await _context.HotelImages.Where(i => i.Id == id).ExecuteDeleteAsync();
        await _s3HotelImageRepository.DeleteAsync(hotelImage.ImageKey.ToString());
    }

    public async Task<HotelImage?> GetValueByIdAsync(int id)
    {
        return await _context.HotelImages.FirstAsync(i => i.Id == id) ?? throw new Exception();
    }

    public async Task<Stream> ReadAsync(int id)
    {
        HotelImage hotelImage = await GetValueByIdAsync(id) ?? throw new Exception();
        return await _s3HotelImageRepository.DownloadAsync(hotelImage.ImageKey.ToString());
    }

    public async Task UpdateAsync(int id, Stream stream)
    {
        HotelImage hotelImage  = await GetValueByIdAsync(id) ?? throw new Exception();
        await _s3HotelImageRepository.UploadAsync(stream, hotelImage.ImageKey.ToString());
    }
}
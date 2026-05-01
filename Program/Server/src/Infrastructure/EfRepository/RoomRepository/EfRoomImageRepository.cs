using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;
using Infrastructure.Database;
using Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfRepository.RoomRepository;

public class EfRoomImageRepository(
    ProgramContext context,
    IS3RoomImageRepository s3RoomImageRepository) : IRoomImageRepository
{
    private readonly ProgramContext _context = context;
    private readonly IS3RoomImageRepository _s3RoomImageRepository = s3RoomImageRepository;
    public async Task CreateAsync(RoomImage entity, Stream stream)
    {
        await _context.RoomImages.AddAsync(entity);
        await _s3RoomImageRepository.UploadAsync(stream, entity.ImageKey.ToString());
    }

    public async Task DeleteAsync(int id)
    {
        RoomImage roomImage = await GetValueByIdAsync(id) ?? throw new Exception();
        await _context.RoomImages.Where(i => i.Id == id).ExecuteDeleteAsync();
        await _s3RoomImageRepository.DeleteAsync(roomImage.ImageKey.ToString());
    }

    public async Task<RoomImage?> GetValueByIdAsync(int id)
    {
        return await _context.RoomImages.FirstAsync(i => i.Id == id) ?? throw new Exception();
    }

    public async Task<Stream> ReadAsync(int id)
    {
        RoomImage roomImage = await GetValueByIdAsync(id) ?? throw new Exception();
        return await _s3RoomImageRepository.DownloadAsync(roomImage.ImageKey.ToString());
    }

    public async Task UpdateAsync(int id, Stream stream)
    {
        RoomImage roomImage  = await GetValueByIdAsync(id) ?? throw new Exception();
        await _s3RoomImageRepository.UploadAsync(stream, roomImage.ImageKey.ToString());
    }
}
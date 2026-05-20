using Domain.Interfaces.Repositories.BaseRepository;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Infrastructure.MinioRepository;

public class MinioRoomImageRepository(
    IS3Repository minioRepository) : IS3RoomImageRepository
{
    private readonly IS3Repository _minioRepository = minioRepository;
    private readonly string bucket = "Room";
    public Task DeleteAsync(string path) => _minioRepository.DeleteAsync(path, bucket);
    public Task<Stream> DownloadAsync(string path) => _minioRepository.DownloadAsync(path, bucket);
    public Task UploadAsync(Stream fileStream, string path) => _minioRepository.UploadAsync(fileStream, path, bucket);
}
using Infrastructure.Interfaces.Repository;

namespace Infrastructure.MinioRepository;

public class MinioHotelImageRepository(
    IS3Repository minioRepository) : IS3HotelImageRepository
{
    private readonly IS3Repository _minioRepository = minioRepository;
    private readonly string bucket = "Hotel";
    public Task DeleteAsync(string fileName) => _minioRepository.DeleteAsync(fileName, bucket);
    public Task<Stream> DownloadAsync(string fileName) => _minioRepository.DownloadAsync(fileName, bucket);
    public Task UploadAsync(Stream fileStream, string fileName) => _minioRepository.UploadAsync(fileStream, fileName, bucket);
}
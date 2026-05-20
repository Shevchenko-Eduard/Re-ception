namespace Domain.Interfaces.Repositories.HotelRepository;

public interface IS3HotelImageRepository
{
    Task UploadAsync(Stream fileStream, string path);
    Task<Stream> DownloadAsync(string path);
    Task DeleteAsync(string path);
}
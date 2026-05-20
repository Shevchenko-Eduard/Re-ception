namespace Domain.Interfaces.Repositories.RoomRepository;

public interface IS3RoomImageRepository
{
    Task UploadAsync(Stream fileStream, string path);
    Task<Stream> DownloadAsync(string path);
    Task DeleteAsync(string path);
}
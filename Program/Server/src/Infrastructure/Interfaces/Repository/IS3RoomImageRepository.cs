namespace Infrastructure.Interfaces.Repository;

public interface IS3RoomImageRepository
{
    Task UploadAsync(Stream fileStream, string fileName);
    Task<Stream> DownloadAsync(string fileName);
    Task DeleteAsync(string fileName);
}
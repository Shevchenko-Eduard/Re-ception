namespace Infrastructure.Interfaces.Repository;

public interface IS3Repository
{
    Task UploadAsync(Stream fileStream, string fileName, string bucket);
    Task<Stream> DownloadAsync(string fileName, string bucket);
    Task DeleteAsync(string fileName, string bucket);
}
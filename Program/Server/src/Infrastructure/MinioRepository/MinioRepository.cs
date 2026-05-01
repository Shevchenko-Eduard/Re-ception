using Infrastructure.Interfaces.Repository;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.MinioRepository;

public class MinioRepository(
    IMinioClient minioClient) : IS3Repository
{    private readonly IMinioClient _minioClient = minioClient;
    public async Task DeleteAsync(string fileName, string bucket)
    {
        var removeArgs = new RemoveObjectArgs()
            .WithBucket(bucket)
            .WithObject(fileName);

        await _minioClient.RemoveObjectAsync(removeArgs);
    }

    public async Task<Stream> DownloadAsync(string fileName, string bucket)
    {
        var memoryStream = new MemoryStream();
        var getObjectArgs = new GetObjectArgs()
            .WithBucket(bucket)
            .WithObject(fileName)
            .WithCallbackStream(stream => stream.CopyTo(memoryStream));

        await _minioClient.GetObjectAsync(getObjectArgs);

        memoryStream.Position = 0;
        return memoryStream;
    }

    public async Task UploadAsync(Stream fileStream, string fileName, string bucket)
    {
        // Проверяем существование бакета
        bool bucketExists = await _minioClient.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(bucket));

        if (!bucketExists)
        {
            // Создаём бакет, если его нет
            await _minioClient.MakeBucketAsync(
                new MakeBucketArgs().WithBucket(bucket));
        }

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucket)
            .WithObject(fileName)
            .WithStreamData(fileStream);

        await _minioClient.PutObjectAsync(putObjectArgs);
    }
}
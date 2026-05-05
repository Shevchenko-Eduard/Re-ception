using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace LibWeb.Services;

/// <summary>
/// Должны быть переменные окружения:
/// Minio__Endpoint в формате host:port,
/// Minio__Username,
/// Minio__Password, 
/// Minio__HTTPS(не обязательно) использовать ли https, по умолчанию false.
/// </summary>
public static class MinioService
{
    public static IServiceCollection AddMinioClient(this IServiceCollection services, IConfiguration configuration)
    {
        var minioSettings = configuration.GetSection("Minio");

        string endpoint = minioSettings["Endpoint"] ?? throw new InvalidOperationException("Minio:Endpoint is missing");
        string username = minioSettings["Username"] ?? throw new InvalidOperationException("Minio:Username is missing");
        string password = minioSettings["Password"] ?? throw new InvalidOperationException("Minio:Password is missing");
        bool https = bool.TryParse(minioSettings["HTTPS"], out var result) && result;

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(endpoint)
            .WithCredentials(username, password)
            .WithSSL(https));

        return services;
    }
}
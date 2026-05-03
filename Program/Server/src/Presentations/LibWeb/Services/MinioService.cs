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
    public static IServiceCollection AddMinioClient(this IServiceCollection services, ConfigurationManager configuration)
    {
        var minioSettings = configuration.GetSection("Minio");

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(minioSettings["Endpoint"])
            .WithCredentials(minioSettings["Username"], minioSettings["Password"])
            .WithSSL(bool.Parse(minioSettings["HTTPS"] ?? "false")));

        return services;
    }
}
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LibWeb.Services;

/// <summary>
/// Должны быть переменные окружения:
/// Redis__Endpoint в формате host:port,
/// Redis__InstanceName (опционально),
/// Redis__Password (опционально)
/// </summary>
public static class RedisService
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettings = configuration.GetSection("Redis");

        string endpoint = redisSettings["Endpoint"] ?? throw new InvalidOperationException("Redis:Endpoint is missing");
        string? instanceName = redisSettings["InstanceName"];
        string? password = redisSettings["Password"];

        var redisConfigurationOptions = new ConfigurationOptions
        {
            EndPoints = { endpoint },
            Password = password
        };

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = endpoint;
            options.InstanceName = instanceName;
            if (!string.IsNullOrEmpty(password))
            {
                options.ConfigurationOptions = redisConfigurationOptions;
            }
        });

        services.AddStackExchangeRedisOutputCache(options =>
        {
            options.Configuration = endpoint;
            options.InstanceName = instanceName;
            if (!string.IsNullOrEmpty(password))
            {
                options.ConfigurationOptions = redisConfigurationOptions;
            }
        });

        var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConfigurationOptions);

        services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(connectionMultiplexer, password)
            .UnprotectKeysWithAnyCertificate(); // Не шифровать ключи

        return services;
    }
}
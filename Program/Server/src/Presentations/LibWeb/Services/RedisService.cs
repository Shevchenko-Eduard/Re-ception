using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = endpoint;
            options.InstanceName = instanceName;
            if (!string.IsNullOrEmpty(password))
            {
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    EndPoints = { endpoint },
                    Password = password
                };
            }
        });

        services.AddStackExchangeRedisOutputCache(options =>
        {
            options.Configuration = endpoint;
            options.InstanceName = instanceName;
            if (!string.IsNullOrEmpty(password))
            {
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                {
                    EndPoints = { endpoint },
                    Password = password
                };
            }
        });

        return services;
    }
}
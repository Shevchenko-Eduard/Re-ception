using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibWeb.Services;

/// <summary>
/// Должны быть переменные окружения:
/// Redis__Endpoint в формате host:port,
/// Redis__InstanceName,
/// </summary>
public static class RedisService
{
    public static IServiceCollection AddRedis(this IServiceCollection services, ConfigurationManager configuration)
    {
        var redisSettings = configuration.GetSection("Redis");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisSettings["Endpoint"];
            options.InstanceName = redisSettings["InstanceName"];
        });

        services.AddStackExchangeRedisOutputCache(options =>
        {
            options.Configuration = redisSettings["Endpoint"];
            options.InstanceName = redisSettings["InstanceName"];
        });
        return services;
    }
}
using Infrastructure.Database;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Strategy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace LibWeb.Services;

/// <summary>
/// Должны быть переменные окружения:
/// DB__Host,
/// DB__Port,
/// DB__Database,
/// DB__Username,
/// DB__Password,
/// </summary>
public static class PostgresService
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IConnectionStrategy>(provider =>
            new PostgresqlStrategy(connectionString: GetConnectionString(configuration)));
        services.AddScoped<ProgramContext>();
        services.AddScoped<DbContext>(sp => sp.GetRequiredService<ProgramContext>());
        services.AddScoped<IDatabaseInitialization, DatabaseInitialization>();
        return services;
    }
    private static string GetConnectionString(ConfigurationManager configuration)
    {
        var dbSettings = configuration.GetSection("DB");
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = dbSettings["Host"] ?? throw new SystemException(),
            Port = int.Parse(dbSettings["Port"] ?? throw new SystemException()),
            Database = dbSettings["Database"] ?? throw new SystemException(),
            Username = dbSettings["Username"] ?? throw new SystemException(),
            Password = dbSettings["Password"] ?? throw new SystemException()
        };
        return builder.ToString();
    }
}
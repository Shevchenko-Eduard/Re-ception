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
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IConnectionStrategy>(provider =>
            new PostgresqlStrategy(connectionString: GetConnectionString(configuration)));
        services.AddScoped<ProgramContext>();
        services.AddScoped<DbContext>(sp => sp.GetRequiredService<ProgramContext>());
        services.AddScoped<IDatabaseInitialization, DatabaseInitialization>();
        return services;
    }
    private static string GetConnectionString(IConfiguration configuration)
    {
        var dbSettings = configuration.GetSection("DB");

        string host = dbSettings["Host"] ?? throw new InvalidOperationException("Postgres:Host is missing");
        int port = int.Parse(dbSettings["Port"] ?? throw new InvalidOperationException("Postgres:Port is missing"));
        string database = dbSettings["Database"] ?? throw new InvalidOperationException("Postgres:Database is missing");
        string username = dbSettings["Username"] ?? throw new InvalidOperationException("Postgres:Username is missing");
        string password = dbSettings["Password"] ?? throw new InvalidOperationException("Postgres:Password is missing");

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = port,
            Database = database,
            Username = username,
            Password = password
        };

        return builder.ToString();
    }
}
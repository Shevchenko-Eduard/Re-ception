using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Database;
using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DependencyInjection;

public static partial class DependencyInjectionConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClock, Clock>();
        
        services.AddScoped<IConnectionStrategy, DiPostgresqlStrategy>();
        services.AddScoped<ProgramContext>();
        services.AddScoped<DbContext, ProgramContext>();

        services.AddScoped<ProgramContext>();

        services.AddScoped<Infrastructure.Interfaces.IHostEnvironment, HostEnvironment>();

        services.AddScoped<IDatabaseInitialization, DatabaseInitialization>();

        return services;
    }
}
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Service;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LibWeb.DependencyInjection;

public static partial class DependencyInjectionConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClock, Clock>();

        services.AddScoped<ICalculatorReservationPrice, CalculatorReservationPrice>();

        services.AddScoped<Infrastructure.Interfaces.IHostEnvironment, HostEnvironment>();

        services.AddScoped<ICurrentUser, CurrentUserService>();

        return services;
    }
}
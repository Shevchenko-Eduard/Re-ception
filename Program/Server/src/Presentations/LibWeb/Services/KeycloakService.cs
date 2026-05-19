using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using LibWeb.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibWeb.Services;

/// <summary>
/// Должны быть переменные окружения:
/// Keycloak__AuthServerUrl,
/// Keycloak__Realm,
/// Keycloak__Resource,
/// Keycloak__Secret,
/// Keycloak__SslRequired(не обязательно),
/// Keycloak__VerifyTokenAudience(не обязательно).
/// </summary>
public static class KeycloakService
{
    public static IServiceCollection AddKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        KeycloakSchema schema = new(configuration);
        services.AddKeycloakAuthentication(schema);
        services.AddKeycloakAuthorization(schema);
        return services;
    }
    public static IServiceCollection AddKeycloakAuthentication(
        this IServiceCollection services, KeycloakSchema schema)
    {
        services
            .AddAuthentication()
            .AddKeycloakWebApi(configureKeycloakOptions: options =>
        {
            options.AuthServerUrl = schema.AuthServerUrl;
            options.Realm = schema.Realm;
            options.Resource = schema.Resource;
            options.Credentials = new() { Secret = schema.Secret };
            options.SslRequired = schema.SslRequired;
            options.VerifyTokenAudience = schema.VerifyTokenAudience;
        });

        return services;
    }
    public static IServiceCollection AddKeycloakAuthorization(
        this IServiceCollection services, KeycloakSchema schema)
    {
        services
            .AddAuthorization()
            .AddKeycloakAuthorization(configureKeycloakAuthorizationOptions: options =>
        {
            options.AuthServerUrl = schema.AuthServerUrl;
            options.Realm = schema.Realm;
            options.Resource = schema.Resource;
            options.Credentials = new() { Secret = schema.Secret };
            options.SslRequired = schema.SslRequired;
            options.VerifyTokenAudience = schema.VerifyTokenAudience;
        })
            .AddAuthorizationBuilder();

        return services;
    }
}
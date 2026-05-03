using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
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
    public static IServiceCollection AddKeycloak(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddKeycloakAuthentication(configuration);
        services.AddKeycloakAuthorization(configuration);
        return services;
    }
    public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keycloakOptions = GetKeycloakInstallationOptions(configuration);

        services
            .AddAuthentication()
            .AddKeycloakWebApi(configureKeycloakOptions: options =>
        {
            options.AuthServerUrl = keycloakOptions.AuthServerUrl;
            options.Realm = keycloakOptions.Realm;
            options.Resource = keycloakOptions.Resource;
            options.Credentials = keycloakOptions.Credentials;
            options.SslRequired = keycloakOptions.SslRequired;
            options.VerifyTokenAudience = keycloakOptions.VerifyTokenAudience;
        });

        return services;
    }
    public static IServiceCollection AddKeycloakAuthorization(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keycloakOptions = GetKeycloakInstallationOptions(configuration);

        services
            .AddAuthorization()
            .AddKeycloakAuthorization(configureKeycloakAuthorizationOptions: options =>
        {
            options.AuthServerUrl = keycloakOptions.AuthServerUrl;
            options.Realm = keycloakOptions.Realm;
            options.Resource = keycloakOptions.Resource;
            options.Credentials = keycloakOptions.Credentials;
            options.SslRequired = keycloakOptions.SslRequired;
            options.VerifyTokenAudience = keycloakOptions.VerifyTokenAudience;
        })
            .AddAuthorizationBuilder();

        return services;
    }
    private static KeycloakInstallationOptions GetKeycloakInstallationOptions(ConfigurationManager configuration)
    {
        var kcSettings = configuration.GetSection("Keycloak");
        var keycloakOptions = new KeycloakInstallationOptions
        {
            AuthServerUrl = kcSettings["AuthServerUrl"] ?? throw new InvalidOperationException("Keycloak:AuthServerUrl is missing"),
            Realm = kcSettings["Realm"] ?? throw new InvalidOperationException("Keycloak:Realm is missing"),
            Resource = kcSettings["Resource"] ?? throw new InvalidOperationException("Keycloak:Resource is missing"),
            Credentials = new KeycloakClientInstallationCredentials
            {
                Secret = kcSettings["Secret"] ?? throw new InvalidOperationException("Keycloak:Secret is missing")
            },
            SslRequired = kcSettings["SslRequired"] ?? "none",
            VerifyTokenAudience = bool.TryParse(kcSettings["VerifyTokenAudience"], out var verify) ? verify : false,
        };
        return keycloakOptions;
    }
}
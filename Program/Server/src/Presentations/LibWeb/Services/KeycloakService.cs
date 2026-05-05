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
    public static IServiceCollection AddKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakAuthentication(configuration);
        services.AddKeycloakAuthorization(configuration);
        return services;
    }
    public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration)
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
    public static IServiceCollection AddKeycloakAuthorization(this IServiceCollection services, IConfiguration configuration)
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
    private static KeycloakInstallationOptions GetKeycloakInstallationOptions(IConfiguration configuration)
    {
        var kcSettings = configuration.GetSection("Keycloak");

        string authServerUrl = kcSettings["AuthServerUrl"] ?? throw new InvalidOperationException("Keycloak:AuthServerUrl is missing");
        string realm = kcSettings["Realm"] ?? throw new InvalidOperationException("Keycloak:Realm is missing");
        string resource = kcSettings["Resource"] ?? throw new InvalidOperationException("Keycloak:Resource is missing");
        string secret = kcSettings["Secret"] ?? throw new InvalidOperationException("Keycloak:Secret is missing");
        string sslRequired = kcSettings["SslRequired"] ?? "none";
        bool verifyTokenAudience = bool.TryParse(kcSettings["VerifyTokenAudience"], out var verify) && verify;

        var keycloakOptions = new KeycloakInstallationOptions
        {
            AuthServerUrl = authServerUrl,
            Realm = realm,
            Resource = resource,
            Credentials = new KeycloakClientInstallationCredentials
            {
                Secret = secret
            },
            SslRequired = sslRequired,
            VerifyTokenAudience = verifyTokenAudience,
        }; 

        return keycloakOptions;
    }
}
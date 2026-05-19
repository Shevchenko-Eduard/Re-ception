using LibWeb.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace LibWeb.Services;

public static class SwaggerService
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type =>
                type.FullName?.Replace("+", ".") ?? type.Name);

            const string referenceId = "Keycloak";

            KeycloakSchema keycloakSchema = new(configuration);

            // Определение схемы безопасности
            options.AddSecurityDefinition(referenceId, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(keycloakSchema.AuthorizationEndpoint().GetAwaiter().GetResult()),
                        TokenUrl = new Uri(keycloakSchema.TokenEndpoint().GetAwaiter().GetResult()),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID" },
                            { "profile", "Profile" },
                            { "roles", "Roles" }
                        }
                    }
                }
            });

            options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference(referenceId),
                    new List<string> { "openid", "profile", "roles" }
                }
            });
        });

        return services;
    }

    public static WebApplication UseSwaggerMap(this WebApplication app, IConfiguration configuration)
    {
        KeycloakSchema schema = new(configuration);

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.OAuthClientId(schema.Resource);
            options.OAuthClientSecret(schema.Secret);
            options.OAuthUsePkce();
        });
        return app;
    }
}
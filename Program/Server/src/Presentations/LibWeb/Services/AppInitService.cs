using LibWeb.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LibWeb.Services;

public static class AppInitService
{
    public static WebApplicationBuilder AddAppInit(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddPostgres(builder.Configuration);
        builder.Services.AddMinioClient(builder.Configuration);
        builder.Services.AddRedis(builder.Configuration);
        builder.Services.AddKeycloak(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddApplicationServices();

        builder.Services.AddHostedService<DbInitService>();

        return builder;
    }
}
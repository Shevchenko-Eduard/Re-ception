using Infrastructure.Database;
using Infrastructure.Database.Interfaces;
using WebApi.DependencyInjection;

namespace WebApi;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationServices();

        builder.Services.AddHealthChecks();

        builder.Services.AddControllers();

        builder.Services.AddLogging();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApi();
        
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseHttpsRedirection();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapHealthChecks("/health");

        // Маппим контроллеры
        app.MapControllers();

        app.Run();
    }
}
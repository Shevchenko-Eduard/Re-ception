using Infrastructure.Database;
using LibWeb.GraphQL;
using LibWeb.Services;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppInit();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services
    .AddGraphQLServer()
    .AddGraphQLQuery()

    .AddAuthorization()

    .AddPagingArguments()
    .AddProjections()
    .AddFiltering()
    .AddSorting()

    .UseQueryCache()
    .AddCacheControl()
    .AddInMemorySubscriptions()

    .RegisterDbContextFactory<ProgramContext>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto |
    ForwardedHeaders.XForwardedHost
});

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerMap(app.Configuration);
}

app.UseOutputCache();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL("/graphql");
app.MapHealthChecks("/health");

app.Run();

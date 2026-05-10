using LibWeb.Services;
using LibWeb.GraphQL;
using Microsoft.AspNetCore.HttpOverrides;
using Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppInit();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type =>
        type.FullName?.Replace("+", ".") ?? type.Name);
});

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services
    .AddGraphQLServer()
    .AddGraphQLQuery()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterDbContextFactory<ProgramContext>();

var app = builder.Build();

app.UseRouting();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto |
    ForwardedHeaders.XForwardedHost
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOutputCache();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL("/graphql");
app.MapHealthChecks("/health");

app.Run();
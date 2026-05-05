using LibWeb.Services;
using LibWeb.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppInit();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services
    .AddGraphQLServer()
    .AddGraphQLQuery()
    .AddProjections()
    .AddFiltering()
    .AddSorting(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseOutputCache(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL("/graphql");
app.MapHealthChecks("/health");

app.Run();

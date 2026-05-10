using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWeb.GraphQL.Hotel;

[ExtendObjectType(typeof(Query))]
public class HotelQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<Domain.Entity.Hotel.Hotel> GetHotels() => _factory.CreateDbContext().Hotels;
}
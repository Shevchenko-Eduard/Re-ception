using Domain.Entity.Hotel;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerWeb.GraphQL.Hotel;

[ExtendObjectType(typeof(Query))]
public class HotelTagQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<HotelTag> GetHotelTags() => _factory.CreateDbContext().HotelTags;
}

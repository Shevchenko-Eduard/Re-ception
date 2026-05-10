using Domain.Entity.Hotel;
using HotChocolate.Data;
using HotChocolate.Types;
using Infrastructure.Database;
using LibWeb.GraphQL;
using Microsoft.EntityFrameworkCore;

namespace CustomerWeb.GraphQL.Hotel;

[ExtendObjectType(typeof(Query))]
public class HotelHotelTagQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<HotelHotelTag> GetHotelHotelTags() => _factory.CreateDbContext().HotelHotelTags;
}

using Domain.Entity.Hotel;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;
using HotChocolate.Caching;

namespace EmployeeWeb.GraphQL.Hotel;

[ExtendObjectType(typeof(Query))]
public class HotelTagQuery : IGraphQLQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [CacheControl(MaxAge = 30)]
    public IQueryable<HotelTag> GetHotelTags([Service] ProgramContext context) => context.HotelTags;
}

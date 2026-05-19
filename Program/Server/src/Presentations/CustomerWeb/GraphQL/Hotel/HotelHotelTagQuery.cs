using Domain.Entity.Hotel;
using HotChocolate;
using HotChocolate.Caching;
using HotChocolate.Data;
using HotChocolate.Types;
using Infrastructure.Database;
using LibWeb.GraphQL;

namespace EmployeeWeb.GraphQL.Hotel;

[ExtendObjectType(typeof(Query))]
public class HotelHotelTagQuery : IGraphQLQuery
{
    [UseProjection]
    [UseFiltering]
    [CacheControl(MaxAge = 30)]
    public IQueryable<HotelHotelTag> GetHotelHotelTags([Service] ProgramContext context) => context.HotelHotelTags;
}

using Domain.Entity.Hotel;
using HotChocolate;
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
    public IQueryable<HotelHotelTag> GetHotelHotelTags([Service] ProgramContext context) => context.HotelHotelTags;
}

using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;

namespace EmployeeWeb.GraphQL.Hotel;

[ExtendObjectType(typeof(Query))]
public class HotelImageQuery : IGraphQLQuery
{

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Domain.Entity.Hotel.HotelImage> GetHotelImages([Service] ProgramContext context) => context.HotelImages;
}
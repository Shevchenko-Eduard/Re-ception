using Domain.Entity.Room;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;

namespace EmployeeWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomTypeQuery : IGraphQLQuery
{

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<RoomType> GetRoomTypes([Service] ProgramContext context) => context.RoomTypes;
}

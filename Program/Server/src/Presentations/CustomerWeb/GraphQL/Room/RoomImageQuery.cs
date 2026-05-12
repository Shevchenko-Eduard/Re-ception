using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;

namespace EmployeeWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomImageQuery : IGraphQLQuery
{

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Domain.Entity.Room.RoomImage> GetRoomImages([Service] ProgramContext context) => context.RoomImages;
}

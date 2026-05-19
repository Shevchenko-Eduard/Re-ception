using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;
using HotChocolate.Caching;

namespace EmployeeWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomImageQuery : IGraphQLQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [CacheControl(MaxAge = 15)]
    public IQueryable<Domain.Entity.Room.RoomImage> GetRoomImages([Service] ProgramContext context) => context.RoomImages;
}

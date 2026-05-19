using Domain.Entity.Room;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;
using HotChocolate.Caching;

namespace EmployeeWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomRoomTagQuery : IGraphQLQuery
{
    [UseProjection]
    [UseFiltering]
    [CacheControl(MaxAge = 30)]
    public IQueryable<RoomRoomTag> GetRoomRoomTags([Service] ProgramContext context) => context.RoomRoomTags;
}

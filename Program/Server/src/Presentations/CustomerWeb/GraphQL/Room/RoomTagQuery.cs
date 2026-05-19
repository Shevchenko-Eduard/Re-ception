using Domain.Entity.Room;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;
using HotChocolate.Caching;

namespace EmployeeWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomTagQuery : IGraphQLQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [CacheControl(MaxAge = 30)]
    public IQueryable<RoomTag> GetRoomTags([Service] ProgramContext context) => context.RoomTags;
}

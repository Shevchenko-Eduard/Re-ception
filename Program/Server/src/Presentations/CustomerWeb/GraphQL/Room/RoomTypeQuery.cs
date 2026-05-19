using Domain.Entity.Room;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;
using HotChocolate.Caching;

namespace EmployeeWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomTypeQuery : IGraphQLQuery
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [CacheControl(MaxAge = 30)]
    public IQueryable<RoomType> GetRoomTypes([Service] ProgramContext context) => context.RoomTypes;
}

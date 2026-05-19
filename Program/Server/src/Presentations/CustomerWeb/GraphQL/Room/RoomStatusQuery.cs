using Domain.Entity.Room;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Caching;

namespace EmployeeWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomStatusQuery : IGraphQLQuery
{
    [UseFiltering]
    [UseSorting]
    [CacheControl(MaxAge = 120)]
    public async Task<IEnumerable<RoomStatus>> GetRoomStatuses([Service] ProgramContext context) => await context.RoomStatuses.ToArrayAsync();
}

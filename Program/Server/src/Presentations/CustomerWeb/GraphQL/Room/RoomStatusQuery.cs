using Domain.Entity.Room;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using HotChocolate;

namespace EmployeeWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomStatusQuery : IGraphQLQuery
{

    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<RoomStatus>> GetRoomStatuses([Service] ProgramContext context)
    {
        return await context.RoomStatuses.ToArrayAsync();
    }
}

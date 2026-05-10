using Domain.Entity.Room;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerWeb.GraphQL.Room;

[ExtendObjectType(typeof(Query))]
public class RoomStatusQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<RoomStatus> GetRoomStatuses() => _factory.CreateDbContext().RoomStatuses;
}

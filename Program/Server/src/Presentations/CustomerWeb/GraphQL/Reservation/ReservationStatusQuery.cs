using Domain.Entity.Reservation;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Caching;

namespace EmployeeWeb.GraphQL.Reservation;

[ExtendObjectType(typeof(Query))]
public class ReservationStatusQuery : IGraphQLQuery
{
    [UseFiltering]
    [UseSorting]
    [CacheControl(MaxAge = 120)]
    public async Task<IEnumerable<ReservationStatus>> GetReservationStatuses([Service] ProgramContext context)
    {
        return await context.ReservationStatuses.ToArrayAsync();
    }
}

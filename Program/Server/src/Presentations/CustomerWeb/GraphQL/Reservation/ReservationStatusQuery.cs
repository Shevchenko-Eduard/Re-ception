using Domain.Entity.Reservation;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using HotChocolate;

namespace EmployeeWeb.GraphQL.Reservation;

[ExtendObjectType(typeof(Query))]
public class ReservationStatusQuery : IGraphQLQuery
{

    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<ReservationStatus>> GetReservationStatuses([Service] ProgramContext context)
    {
        return await context.ReservationStatuses.ToArrayAsync();
    }
}

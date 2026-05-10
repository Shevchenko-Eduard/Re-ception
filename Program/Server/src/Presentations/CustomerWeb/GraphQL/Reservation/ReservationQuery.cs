using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;

namespace EmployeeWeb.GraphQL.Reservation;

[ExtendObjectType(typeof(Query))]
public class ReservationQuery : IGraphQLQuery
{

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Domain.Entity.Reservation.Reservation> GetReservations([Service] ProgramContext context) => context.Reservations;
}

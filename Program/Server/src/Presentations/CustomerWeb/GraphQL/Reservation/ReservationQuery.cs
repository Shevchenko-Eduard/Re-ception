using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;
using Application.Interfaces;
using HotChocolate.Authorization;

namespace EmployeeWeb.GraphQL.Reservation;

[ExtendObjectType(typeof(Query))]
public class ReservationQuery(ICurrentUser currentUser) : IGraphQLQuery
{
    private readonly ICurrentUser _currentUser = currentUser;
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [Authorize]
    public IQueryable<Domain.Entity.Reservation.Reservation> GetReservations([Service] ProgramContext context) => 
        context.Reservations
            .Where(r => r.GuestId == _currentUser.Id);
}

using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWeb.GraphQL.Reservation;

[ExtendObjectType(typeof(Query))]
public class ReservationQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<Domain.Entity.Reservation.Reservation> GetReservations() => _factory.CreateDbContext().Reservations;
}

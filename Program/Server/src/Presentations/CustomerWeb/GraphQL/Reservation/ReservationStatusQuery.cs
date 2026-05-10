using Domain.Entity.Reservation;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerWeb.GraphQL.Reservation;

[ExtendObjectType(typeof(Query))]
public class ReservationStatusQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<ReservationStatus> GetReservationStatuses() => _factory.CreateDbContext().ReservationStatuses;
}

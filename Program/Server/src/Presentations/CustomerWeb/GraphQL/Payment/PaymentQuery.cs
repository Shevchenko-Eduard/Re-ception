using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace EmployeeWeb.GraphQL.Payment;

[ExtendObjectType(typeof(Query))]
public class PaymentQuery(ICurrentUser currentUser) : IGraphQLQuery
{
    private readonly ICurrentUser _currentUser = currentUser;
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [Authorize]
    public IQueryable<Domain.Entity.Payment.Payment> GetPayments([Service] ProgramContext context) =>
        context.Payments
            .Include(p => p.Reservation)
            .Where(p => p.Reservation != null && p.Reservation.GuestId == _currentUser.Id);
}

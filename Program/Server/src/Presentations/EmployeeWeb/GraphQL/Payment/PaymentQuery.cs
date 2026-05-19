using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using HotChocolate;
using HotChocolate.Authorization;

namespace EmployeeWeb.GraphQL.Payment;

[ExtendObjectType(typeof(Query))]
public class PaymentQuery : IGraphQLQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [Authorize]
    public IQueryable<Domain.Entity.Payment.Payment> GetPayments([Service] ProgramContext context) => context.Payments;
}

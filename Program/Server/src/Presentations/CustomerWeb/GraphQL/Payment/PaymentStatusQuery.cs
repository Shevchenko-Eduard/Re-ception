using Domain.Entity.Payment;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using HotChocolate;

namespace EmployeeWeb.GraphQL.Payment;

[ExtendObjectType(typeof(Query))]
public class PaymentStatusQuery : IGraphQLQuery
{

    [UseFiltering]
    [UseSorting] 
    public async Task<IEnumerable<PaymentStatus>> GetPaymentStatuses([Service] ProgramContext context)
    {
        return await context.PaymentStatuses.ToArrayAsync();
    }
}

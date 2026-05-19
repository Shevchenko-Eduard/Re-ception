using Domain.Entity.Payment;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Caching;

namespace EmployeeWeb.GraphQL.Payment;

[ExtendObjectType(typeof(Query))]
public class PaymentStatusQuery : IGraphQLQuery
{
    [UseFiltering]
    [UseSorting] 
    [CacheControl(MaxAge = 120)]
    public async Task<IEnumerable<PaymentStatus>> GetPaymentStatuses([Service] ProgramContext context)
    {
        return await context.PaymentStatuses.ToArrayAsync();
    }
}

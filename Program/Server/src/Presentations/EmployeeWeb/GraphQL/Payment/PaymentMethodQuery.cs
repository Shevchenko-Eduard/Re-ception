using Domain.Entity.Payment;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using HotChocolate;

namespace EmployeeWeb.GraphQL.Payment;

[ExtendObjectType(typeof(Query))]
public class PaymentMethodQuery : IGraphQLQuery
{

    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods([Service] ProgramContext context)
    {
        return await context.PaymentMethods.ToArrayAsync();
    }
}

using Domain.Entity.Payment;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWeb.GraphQL.Payment;

[ExtendObjectType(typeof(Query))]
public class PaymentStatusQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<PaymentStatus> GetPaymentStatuses() => _factory.CreateDbContext().PaymentStatuses;
}

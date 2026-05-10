using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerWeb.GraphQL.Payment;

[ExtendObjectType(typeof(Query))]
public class PaymentQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<Domain.Entity.Payment.Payment> GetPayments() => _factory.CreateDbContext().Payments;
}

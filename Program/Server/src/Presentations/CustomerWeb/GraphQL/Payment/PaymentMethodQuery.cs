using Domain.Entity.Payment;
using HotChocolate.Data;
using HotChocolate.Types;
using LibWeb.GraphQL;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CustomerWeb.GraphQL.Payment;

[ExtendObjectType(typeof(Query))]
public class PaymentMethodQuery(IDbContextFactory<ProgramContext> factory) : IGraphQLQuery
{
    private readonly IDbContextFactory<ProgramContext> _factory = factory;

    [UseProjection]
    [UseFiltering]
    [UseSorting] 
    public IQueryable<PaymentMethod> GetPaymentMethods() => _factory.CreateDbContext().PaymentMethods;
}

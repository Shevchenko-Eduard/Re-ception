using System.Reflection;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibWeb.GraphQL;

public interface IGraphQLQuery { }


public static class GraphQLQuery
{
    public static IRequestExecutorBuilder AddGraphQLQuery(this IRequestExecutorBuilder request, Assembly? assembly = null, string? version = null)
    {
        Assembly targetAssembly = assembly ?? Assembly.GetCallingAssembly();

        var queryTypesWithAttr = targetAssembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false, IsPublic: true } &&
                       typeof(IGraphQLQuery).IsAssignableFrom(t));

        if (!string.IsNullOrEmpty(version))
        {
            Query.Version = version;
        }

        request.AddQueryType<Query>();
        request.AddTypeExtension<EmailExtensions>();
        request.AddTypeExtension<PhoneExtensions>();

        foreach (var type in queryTypesWithAttr)
        {
            request.AddTypeExtension(type);
        }

        return request;
    }
}
using System.Reflection;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibWeb.GraphQL;

public interface IGraphQLQuery { }

public static class GraphQLQuery
{
    public static IRequestExecutorBuilder AddGraphQLQuery(this IRequestExecutorBuilder request, Assembly? assembly = null)
    {
        Assembly targetAssembly = assembly ?? Assembly.GetCallingAssembly();

        var queryTypesWithAttr = targetAssembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false, IsPublic: true } && 
                       typeof(IGraphQLQuery).IsAssignableFrom(t));

        foreach (var type in queryTypesWithAttr)
        {
            request.AddQueryType(type);
        }

        return request;
    }

    public static IRequestExecutorBuilder AddGraphQLQuery<TMarker>(this IRequestExecutorBuilder request)
    {
        return request.AddGraphQLQuery(typeof(TMarker).Assembly);
    }

    public static IRequestExecutorBuilder AddGraphQLQueries(
        this IRequestExecutorBuilder request,
        params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            request.AddGraphQLQuery(assembly);
        }
        return request;
    }
}
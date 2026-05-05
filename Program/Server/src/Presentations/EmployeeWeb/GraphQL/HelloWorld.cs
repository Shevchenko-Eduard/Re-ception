using LibWeb.GraphQL;

namespace EmployeeWeb.GraphQL;

public class HelloWorld : IGraphQLQuery
{
    public string GetHello() => "Hello from graphql!";
}
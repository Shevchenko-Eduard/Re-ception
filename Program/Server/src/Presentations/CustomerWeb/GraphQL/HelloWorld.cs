using LibWeb.GraphQL;

namespace CustomerWeb.GraphQL;

public class HelloWorld : IGraphQLQuery
{
    public string GetHello() => "Hello from graphql!";
}
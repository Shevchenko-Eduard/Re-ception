using Domain.Entity;

namespace LibWeb.GraphQL;

[ExtendObjectType(typeof(Email))]
public class EmailExtensions
{
    public static string GetValue([Parent] Email email) => email.ToString();
}
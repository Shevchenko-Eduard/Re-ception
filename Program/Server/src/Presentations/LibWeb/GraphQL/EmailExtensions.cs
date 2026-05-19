using Domain.Entity;

namespace LibWeb.GraphQL;

[ExtendObjectType(typeof(Email))]
#pragma warning disable RCS1102 // Make class static
public class EmailExtensions
#pragma warning restore RCS1102 // Make class static
{
    public static string GetValue([Parent] Email email) => email.ToString();
}
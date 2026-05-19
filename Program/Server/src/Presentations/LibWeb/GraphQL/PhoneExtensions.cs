using Domain.Entity;

namespace LibWeb.GraphQL;

[ExtendObjectType(typeof(Phone))]
#pragma warning disable RCS1102 // Make class static
public class PhoneExtensions
#pragma warning restore RCS1102 // Make class static
{
    public static string GetValue([Parent] Phone phone) => phone.ToString();
}
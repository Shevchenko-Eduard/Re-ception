using Domain.Entity;

namespace LibWeb.GraphQL;

[ExtendObjectType(typeof(Phone))]
public class PhoneExtensions
{
    public static string GetValue([Parent] Phone phone) => phone.ToString();
}
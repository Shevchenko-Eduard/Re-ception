namespace LibWeb.GraphQL;

public  class Query
{
    public static string Version { get; set; } = "1.0.0";
    public string GetVersion() => Version;
}
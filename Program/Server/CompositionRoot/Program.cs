var allTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes());
foreach (Type type in allTypes)
{
    Console.WriteLine(type.ToString());
    foreach(var method in type.GetMethods())
    {
        Console.WriteLine($"  - {method.ToString()}");
    }
}
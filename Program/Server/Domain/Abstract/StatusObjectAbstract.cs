namespace Domain.Abstract;

public abstract class StatusObjectAbstract<T> : EnumObjectAbstract<T> where T : StatusObjectAbstract<T>
{
    public string Name { get; init; }
    protected StatusObjectAbstract(int id, string name) : base(id)
    {
        Name = name;
    }
    public static StatusObjectAbstract<T> FromName(string name)
    {
        return All.FirstOrDefault(g => g.Name == name)
            ?? throw new ArgumentException($"Invalid gender name: {name}");
    }
}
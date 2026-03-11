namespace Domain.Entity;

public class Gender
{
    public ushort Id { get; init; }
    public string Name { get; init; }
    private Gender(ushort id, string name)
    {
        Id = id;
        Name = name;
    }
    public static readonly Gender Indeterminate = new(0, "Indeterminate");
    public static readonly Gender Female = new(1, "Female");
    public static readonly Gender Male = new(2, "Male");
    public static readonly List<Gender> All = [Indeterminate, Female, Male];
    public static Gender FromName(string name)
    {
        return All.FirstOrDefault(g => g.Name == name)
            ?? throw new ArgumentException($"Invalid gender name: {name}");
    }
    public static Gender FromId(ushort id)
    {
        return All.FirstOrDefault(g => g.Id == id)
            ?? throw new ArgumentException($"Invalid gender id: {id}");
    }
}
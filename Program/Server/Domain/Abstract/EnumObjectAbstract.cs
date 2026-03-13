using System.Reflection;

namespace Domain.Abstract;

public abstract class EnumObjectAbstract<T> where T : EnumObjectAbstract<T>
{
    public byte Id {get; init;}
    public static readonly HashSet<T> All = typeof(T)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Select(f => (T)f?.GetValue(null)! ?? throw new SystemException())
        ?.ToHashSet() ?? throw new SystemException();
    protected EnumObjectAbstract(int id)
    {
        Id = (byte)id;
    }
    public static EnumObjectAbstract<T> FromId(ushort id)
    {
        return All.FirstOrDefault(g => g.Id == id)
            ?? throw new ArgumentException($"Invalid gender id: {id}");
    }
}
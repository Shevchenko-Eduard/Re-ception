namespace UnitTests;

public static class StringTests
{
    public static string? Length(int? length)
    {
        string? value = null;
        if (length is not null)
        {
            value = new(new char[(int)length]);
        }
        return value;
    }
    public static string Length(int length)
    {
        return Length((int?)length)!;
    }
}
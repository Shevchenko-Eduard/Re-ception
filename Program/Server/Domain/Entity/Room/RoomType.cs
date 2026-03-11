namespace Domain.Entity.Room;

public sealed class RoomType
{
    private const ushort _maxName = 50;
    private const ushort _maxDescription = 250;
    public ushort Id { get; private set; }
    public string Name
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxName)
            {
                throw new ArgumentException(message: $"The name must not exceed {_maxName} characters.");
            }
            field = value;
        }
    }
    public string Description
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxDescription)
            {
                throw new ArgumentException(message: $"The description must not exceed {_maxDescription} characters.");
            }
            field = value;
        }
    }
    public decimal BasePrice
    {
        get; private set
        {
            if (value < 0)
            {
                throw new ArgumentException(message: "The price cannot be negative");
            }
            field = value;
        }
    }
    public ushort MaxCountGuest { get; private set; }
#pragma warning disable CS9264
    private RoomType() { }
#pragma warning restore CS9264
    public RoomType(
        string name, string description,
        decimal basePrice, int maxGuest)
    {
        Name = name;
        Description = description;
        BasePrice = basePrice;
        checked { MaxCountGuest = (ushort)maxGuest; }
    }
}
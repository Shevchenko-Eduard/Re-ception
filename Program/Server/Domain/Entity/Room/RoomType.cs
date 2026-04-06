namespace Domain.Entity.Room;

public sealed class RoomType
{
    #region Constants
    private const ushort _maxName = 50;
    private const ushort _maxDescription = 250;
    #endregion
    #region Fields
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
    } = null!;
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
    } = null!;
    public decimal BasePricePerDay
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
    #endregion
    #region Navigation properties
    public ICollection<Room>? Rooms { get; private set; }
    #endregion
    #region Constructors
    private RoomType() { }
    public RoomType(
        string name, string description,
        decimal basePrice, int maxGuest)
    {
        Name = name;
        Description = description;
        BasePricePerDay = basePrice;
        checked { MaxCountGuest = (ushort)maxGuest; }
    }
    #endregion
    #region Methods
    public void UpdateName(string name)
    {
        Name = name;
    }
    public void UpdateDescription(string description)
    {
        Description = description;
    }
    public void UpdateBasePricePerDay(decimal basePricePerDay)
    {
        BasePricePerDay = basePricePerDay;
    }
    public void UpdateMaxCountGuest(int maxCountGuest)
    {
        checked { MaxCountGuest = (ushort)maxCountGuest; }
    }
    #endregion
}
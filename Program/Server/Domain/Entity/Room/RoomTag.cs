namespace Domain.Entity.Room;

public sealed class RoomTag
{
    #region Constants
    private const ushort _maxName = 50;
    private const ushort _maxDescription = 250;
    #endregion
    #region Fields
    public ushort Id { get; init; }
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
    #endregion
    #region Navigation properties
    public ICollection<Room>? Rooms { get; private set; }
    #endregion
    #region Constructors
    private RoomTag() { }
    public RoomTag(string name, string description)
    {
        Name = name;
        Description = description;
    }
    #endregion
}
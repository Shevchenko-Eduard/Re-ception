using Domain.Exception;

namespace Domain.Entity.Hotel;

public sealed class Tag
{
    #region Constants
    private const ushort _maxName = 50;
    private const ushort _maxDescription = 250;
    #endregion
    #region Fields
    public int Id { get; init; }
    public string Name
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxName)
            {
                throw new DomainExternalException(message: $"The name must not exceed {_maxName} characters.");
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
                throw new DomainExternalException(message: $"The description must not exceed {_maxDescription} characters.");
            }
            field = value;
        }
    } = null!;
    #endregion
    #region Navigation properties
    public ICollection<HotelTag>? HotelTags { get; private set; }
    #endregion
    #region Constructors
    private Tag() { }
    public Tag(string name, string description)
    {
        Name = name;
        Description = description;
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
    #endregion
}
namespace Domain.Entity.Hotel;

public sealed class HotelTag
{
    #region Constants
    #endregion
    #region Fields
    public int Id { get; init; }
    public string Name
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            field = value;
        }
    } = null!;
    public string Description
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            field = value;
        }
    } = null!;
    #endregion
    #region Navigation properties
    public ICollection<HotelHotelTag>? HotelHotelTags { get; private set; }
    #endregion
    #region Constructors
    private HotelTag() { }
    public HotelTag(string name, string description)
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
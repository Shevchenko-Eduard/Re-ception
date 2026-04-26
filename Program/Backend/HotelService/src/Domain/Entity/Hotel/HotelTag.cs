namespace Domain.Entity.Hotel;

public class HotelTag
{
    #region Constants
    #endregion
    #region Fields
    public int Id { get; init; }
    public int HotelId { get; init; }
    public int TagId { get; init; }
    #endregion
    #region Navigation properties
    public Hotel? Hotel { get; private set; }
    public Tag? Tag { get; private set; }
    #endregion
    #region Constructors
    private HotelTag() { }
    public HotelTag(int hotelId, int tagId)
    {
        HotelId = hotelId;
        TagId = tagId;
    }
    #endregion
    #region Methods
    #endregion
}
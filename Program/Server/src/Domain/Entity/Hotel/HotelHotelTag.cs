namespace Domain.Entity.Hotel;

public class HotelHotelTag
{
    #region Constants
    #endregion
    #region Fields
    public int Id { get; init; }
    public int HotelId { get; init; }
    public int HotelTagId { get; init; }
    #endregion
    #region Navigation properties
    public Hotel? Hotel { get; private set; }
    public HotelTag? HotelTag { get; private set; }
    #endregion
    #region Constructors
    private HotelHotelTag() { }
    public HotelHotelTag(int hotelId, int tagId)
    {
        HotelId = hotelId;
        HotelTagId = tagId;
    }
    #endregion
    #region Methods
    #endregion
}
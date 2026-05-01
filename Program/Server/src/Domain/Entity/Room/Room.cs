namespace Domain.Entity.Room;

public sealed class Room
{
    #region Fields
    public int Id { get; init; }
    public int HotelId { get; private set; }
    public int RoomTypeId { get; private set; }
    public int RoomNumber { get; private set; }
    public int Floor { get; private set; }
    public byte RoomStatusId { get; private set; }
    public decimal? PricePerDay { get; private set; }
    #endregion
    #region Navigation properties
    public Hotel.Hotel? Hotel { get; private set; }
    public RoomType? RoomType
    {
        get; private set
        {
            if (value?.Id != RoomTypeId)
            {
                throw new ArgumentException();
            }
            field = value;
        }
    }
    public ICollection<RoomRoomTag>? RoomRoomTags { get; private set; }
    public ICollection<Reservation.Reservation>? Reservations { get; private set; }
    public RoomStatus? RoomStatus { get; private set; }
    public ICollection<RoomImage>? RoomImages { get; private set; }
    #endregion
    #region Constructors
    private Room() { }
    public Room(
        int hotelId, int roomTypeId,
        int roomNumber, int floor,
        int roomStatusId,
        decimal? pricePerDay = null)
    {
        HotelId = hotelId;
        RoomTypeId = roomTypeId;
        RoomNumber = roomNumber;
        Floor = floor;
        RoomStatusId = (byte)roomStatusId;
        PricePerDay = pricePerDay;
    }
    #endregion
    #region Methods
    public void SetRoomType(RoomType roomType) => RoomType = roomType;
    public void UpdateRoomTypeId(int roomTypeId) => RoomTypeId = roomTypeId;
    public void UpdateRoomNumber(int roomNumber) => RoomNumber = roomNumber;
    public void UpdateFloor(int floor) => Floor = floor;
    public void UpdateRoomStatusId(byte roomStatusId) => RoomStatusId = roomStatusId;
    public void UpdatePricePerDay(decimal? pricePerDay) => PricePerDay = pricePerDay;
    #endregion
}
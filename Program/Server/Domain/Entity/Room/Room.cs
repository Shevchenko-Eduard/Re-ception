namespace Domain.Entity.Room;

public sealed class Room
{
    #region Fields
    public ushort Id { get; init; }
    public byte HotelId { get; private set; }
    public ushort RoomTypeId { get; private set; }
    public ushort RoomNumber { get; private set; }
    public byte Floor { get; private set; }
    public ushort RoomStatusId { get; private set; }
    public decimal? PricePerDay { get; private set; }
    #endregion
    #region Navigation properties
    // Подгружаемые данные
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
    public ICollection<RoomTag>? RoomTags { get; private set; }
    public ICollection<Reservation.Reservation>? Reservations { get; private set; }
    public RoomStatus? RoomStatus { get; private set; }
    #endregion
    #region Constructors
    private Room() { }
    public Room(
        int hotelId, int roomTypeId,
        int roomNumber, int floor,
        int roomStatusId,
        decimal? pricePerDay = null)
    {
        HotelId = (byte)hotelId;
        RoomTypeId = (ushort)roomTypeId;
        RoomNumber = (ushort)roomNumber;
        Floor = (byte)floor;
        RoomStatusId = (ushort)roomStatusId;
        PricePerDay = pricePerDay;
    }
    #endregion
    #region Methods
    public void SetRoomType(RoomType roomType)
    {
        RoomType = roomType;
    }
    public void UpdateRoomTypeId(ushort roomTypeId)
    {
        RoomTypeId = roomTypeId;
    }
    public void UpdateRoomNumber(ushort roomNumber)
    {
        RoomNumber = roomNumber;
    }
    public void UpdateFloor(byte floor)
    {
        Floor = floor;
    }
    public void UpdateRoomStatusId(ushort roomStatusId)
    {
        RoomStatusId = roomStatusId;
    }
    public void UpdatePricePerDay(decimal? pricePerDay)
    {
        PricePerDay = pricePerDay;
    }
    public void AddRoomTag(RoomTag roomTag)
    {
        RoomTags ??= new List<RoomTag>();
        RoomTags.Add(roomTag);
    }
    public void RemoveRoomTag(RoomTag roomTag)
    {
        RoomTags?.Remove(roomTag);
    }
    #endregion
}
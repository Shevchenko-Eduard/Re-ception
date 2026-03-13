namespace Domain.Entity.Room;

public sealed class Room
{
    #region Fields
    public ushort Id { get; init; }
    public byte HotelId { get; private set; }
    public ushort TypeRomeId { get; private set; }
    public ushort RoomNumber { get; private set; }
    public byte Floor { get; private set; }
    public ushort RoomStatusId { get; private set; }
    #endregion
    #region Navigation properties
    // Подгружаемые данные
    public Hotel.Hotel? Hotel { get; private set; }
    public RoomType? RoomType { get; private set; }
    public List<RoomTag>? RoomTags { get; private set; }
    public RoomStatus? RoomStatus { get; private set; }
    #endregion
    #region Constructors
    private Room() { }
    public Room(
        int hotelId, int typeRomeId,
        int roomNumber, int floor,
        int roomStatusId)
    {
        HotelId = (byte)hotelId;
        TypeRomeId = (ushort)typeRomeId;
        RoomNumber = (ushort)roomNumber;
        Floor = (byte)floor;
        RoomStatusId = (ushort)roomStatusId;
    }
    #endregion
}
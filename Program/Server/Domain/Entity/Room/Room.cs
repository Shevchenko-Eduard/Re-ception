namespace Domain.Entity.Room;

public sealed class Room
{
    public uint Id { get; private set; }
    public ushort HotelId { get; private set; }
    public ushort TypeRomeId { get; private set; }
    public ushort RoomNumber { get; private set; }
    public ushort Floor { get; private set; }
    public List<RoomTag> RoomTags { get; private set; }
    public RoomStatusEnum RoomStatus { get; private set; }
#pragma warning disable CS8618
    private Room() { }
#pragma warning restore CS8618
    private Room(
        int hotelId, int typeRomeId,
        int roomNumber, int floor,
        IEnumerable<RoomTag> roomTags,
        RoomStatusEnum roomStatus)
    {
        checked
        {
            HotelId = (ushort)hotelId;
            TypeRomeId = (ushort)typeRomeId;
            RoomNumber = (ushort)roomNumber;
            Floor = (ushort)floor;
        }
        RoomStatus = roomStatus;
        RoomTags = (List<RoomTag>)roomTags;
    }
}
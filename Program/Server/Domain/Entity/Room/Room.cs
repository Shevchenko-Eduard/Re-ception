namespace Domain.Entity.Room;

public sealed class Room
{
    public uint Id { get; init; }
    public ushort HotelId { get; private set; }
    public ushort TypeRomeId { get; private set; }
    public ushort RoomNumber { get; private set; }
    public ushort Floor { get; private set; }
    public List<RoomTag>? RoomTags { get; private set; }
    public ushort RoomStatusId { get; private set; }
    public RoomStatus? RoomStatus { get; private set; }
#pragma warning disable CS8618
    private Room() { }
#pragma warning restore CS8618
    public Room(
        int hotelId, int typeRomeId,
        int roomNumber, int floor,
        int roomStatusId)
    {
        checked
        {
            HotelId = (ushort)hotelId;
            TypeRomeId = (ushort)typeRomeId;
            RoomNumber = (ushort)roomNumber;
            Floor = (ushort)floor;
        }
        RoomStatusId = (ushort)roomStatusId;
    }
}
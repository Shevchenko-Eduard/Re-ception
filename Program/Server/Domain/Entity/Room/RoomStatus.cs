namespace Domain.Entity.Room;

public sealed class RoomStatus
{
    public ushort Id { get; init; }
    public string Name { get; init; }
    private RoomStatus(ushort id, string name)
    {
        Id = id;
        Name = name;
    }
    public static readonly RoomStatus Vacant = new(0, "Vacant");
    public static readonly RoomStatus CheckOut = new(1, "CheckOut");
    public static readonly RoomStatus OutOfOrder = new(2, "OutOfOrder");
    public static readonly RoomStatus Occupied = new(3, "Occupied");
    public static readonly RoomStatus Reserved = new(4, "Reserved");
    public static readonly List<RoomStatus> All = [Vacant, CheckOut, OutOfOrder, Occupied, Reserved];
    public static RoomStatus FromName(string name)
    {
        return All.FirstOrDefault(s => s.Name == name)
            ?? throw new ArgumentException($"Invalid room status name: {name}");
    }
    public static RoomStatus FromId(ushort id)
    {
        return All.FirstOrDefault(s => s.Id == id)
            ?? throw new ArgumentException($"Invalid room status id: {id}");
    }
}
using Domain.Abstract;

namespace Domain.Entity.Room;

public sealed class RoomStatus : StatusObjectAbstract<RoomStatus>
{
    #region Constructors
    private RoomStatus(ushort id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly RoomStatus Vacant = new(0, "Vacant");
    public static readonly RoomStatus CheckOut = new(1, "CheckOut");
    public static readonly RoomStatus OutOfOrder = new(2, "OutOfOrder");
    public static readonly RoomStatus Occupied = new(3, "Occupied");
    public static readonly RoomStatus Reserved = new(4, "Reserved");
    #endregion
    #region Navigation properties
    public ICollection<Room>? Rooms { get; private set; }
    #endregion
}
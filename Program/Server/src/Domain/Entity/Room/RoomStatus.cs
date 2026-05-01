using Domain.Abstract;

namespace Domain.Entity.Room;

public sealed class RoomStatus : StatusObjectAbstract<RoomStatus>
{
    #region Constructors
    private RoomStatus(string name) : base(name) { }
    #endregion
    #region Default objects
    public static readonly RoomStatus Vacant = new("Vacant");
    public static readonly RoomStatus CheckOut = new("CheckOut");
    public static readonly RoomStatus OutOfOrder = new("OutOfOrder");
    public static readonly RoomStatus Occupied = new("Occupied");
    public static readonly RoomStatus Reserved = new("Reserved");
    #endregion
    #region Navigation properties
    public ICollection<Room>? Rooms { get; private set; }
    #endregion
}
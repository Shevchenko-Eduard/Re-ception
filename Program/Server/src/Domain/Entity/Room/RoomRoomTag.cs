namespace Domain.Entity.Room;

public class RoomRoomTag
{
    #region Constants
    #endregion
    #region Fields
    public int Id { get; init; }
    public int RoomId { get; init; }
    public int RoomTagId { get; init; }
    #endregion
    #region Navigation properties
    public Room? Room { get; private set; }
    public RoomTag? RoomTag { get; private set; }
    #endregion
    #region Constructors
    private RoomRoomTag() { }
    public RoomRoomTag(int roomId, int roomTagId)
    {
        RoomId = roomId;
        RoomTagId = roomTagId;
    }
    #endregion
    #region Methods
    #endregion
}
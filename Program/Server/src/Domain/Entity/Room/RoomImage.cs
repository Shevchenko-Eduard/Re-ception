namespace Domain.Entity.Room;

public class RoomImage
{
    #region Constants
    #endregion
    #region Fields
    public int Id { get; init; }
    public int RoomId { get; init; }
    public Guid ImageKey { get; init; }
    #endregion
    #region Navigation properties
    public Room? Room { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
    private RoomImage() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
    public RoomImage(int roomId)
    {
        RoomId = roomId;
        ImageKey = Guid.NewGuid();
    }
    #endregion
    #region Methods
    #endregion
}
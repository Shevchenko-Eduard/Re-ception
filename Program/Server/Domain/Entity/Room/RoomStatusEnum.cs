namespace Domain.Entity.Room;

public enum RoomStatusEnum
{
    /// <summary>
    /// Номер свободен и готов к заселению.
    /// </summary>
    Vacant,
    /// <summary>
    /// Номер свободен но требуется его обслужить перед повторным заселением.
    /// </summary>
    CheckOut,
    /// <summary>
    /// Номер вне эксплуатации по какой-то причине.
    /// </summary>
    OutOfOrder,
    /// <summary>
    /// Номер занят.
    /// </summary>
    Occupied,
    /// <summary>
    /// Номер забронирован.
    /// </summary>
    Reserved
}
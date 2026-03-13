using Domain.Entity.Guest;
using Domain.Entity.Room;

namespace Domain.Entity.Reservation;

public class Reservation
{
    #region Fields
    public ulong Id { get; init; }
    public Guid GuestId { get; init; }
    public uint RoomId { get; init; }
    public DateTimeOffset CheckIn { get; private set; }
    public DateTimeOffset CheckOut { get; private set; }
    public byte ReservationStatusId { get; private set; }
    public DateTimeOffset CreateAt { get; init; }
    public decimal TotalPrice { get; init; }
    #endregion
    #region Navigation properties
    public Guest.Guest? Guest { get; private set; }
    public Room.Room? Room { get; private set; }
    public ReservationStatus? ReservationStatus { get; private set; }
    #endregion
    #region Constructors
    public Reservation() { }
    public Reservation(
        Guid guestId, uint roomId,
        DateTime checkIn, DateTime checkOut,
        int reservationStatusId,
        decimal totalPrice)
    {
        GuestId = guestId;
        RoomId = roomId;
        CheckIn = checkIn;
        CheckOut = checkOut;
        ReservationStatusId = (byte)reservationStatusId;
        TotalPrice = totalPrice;
    }
    #endregion
}
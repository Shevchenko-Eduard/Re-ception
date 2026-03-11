namespace Domain.Entity.Reservation;

public class Reservation
{
    public ulong Id { get; private set; }
    public ulong GuestId { get; private set; }
    public uint RoomId { get; private set; }
    public DateTimeOffset CheckIn { get; private set; }
    public DateTimeOffset CheckOut { get; private set; }
    public ushort ReservationStatusId { get; private set; }
    public ReservationStatus? ReservationStatus { get; private set; }
    public DateTimeOffset CreateAt { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Reservation() { }
    public Reservation(
        ulong guestId, uint roomId,
        DateTime checkIn, DateTime checkOut,
        int reservationStatusId,
        decimal totalPrice)
    {
        GuestId = guestId;
        RoomId = roomId;
        CheckIn = checkIn;
        CheckOut = checkOut;
        ReservationStatusId = (ushort)reservationStatusId;
        TotalPrice = totalPrice;
    }
}
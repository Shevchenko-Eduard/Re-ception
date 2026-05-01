namespace Domain.Entity.Payment;

public class Payment
{
    #region Fields
    public int Id { get; init; }
    public byte StatusId { get; init; }
    public byte MethodId { get; init; }
    public decimal Amount { get; init; }
    public int? ReservationId { get; init; }
    public DateTimeOffset PaymentDate { get; init; }
    #endregion
    #region Navigation properties
    public Reservation.Reservation? Reservation { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public PaymentStatus? PaymentStatus { get; private set; }
    #endregion
    #region Constructors
    public Payment() { }
    public Payment(
        int reservationId,
        int statusId,
        int methodId,
        decimal amount,
        DateTimeOffset paymentDate)
    {
        ReservationId = reservationId;
        StatusId = (byte)statusId;
        MethodId = (byte)methodId;
        Amount = amount;
        PaymentDate = paymentDate;
    }
    #endregion
}
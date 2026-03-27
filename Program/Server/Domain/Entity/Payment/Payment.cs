namespace Domain.Entity.Payment;

public class Payment
{
    #region Fields
    public uint Id { get; init; }
    public byte StatusId { get; init; }
    public byte MethodId { get; init; }
    public decimal Amount { get; init; }
    public DateTimeOffset PaymentDate { get; init; }
    #endregion
    #region Navigation properties
    public ICollection<Reservation.Reservation>? Reservations { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public PaymentStatus? PaymentStatus { get; private set; }
    #endregion
    #region Constructors
    public Payment(
        uint id,
        int statusId,
        int methodId,
        decimal amount,
        DateTimeOffset paymentDate)
    {
        Id = id;
        StatusId = (byte)statusId;
        MethodId = (byte)methodId;
        Amount = amount;
        PaymentDate = paymentDate;
    }
    #endregion
}
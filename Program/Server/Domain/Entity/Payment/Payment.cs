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
    #region Navigation Properties
    public PaymentStatus? Status
    {
        get; private set
        {
            if (value is null)
            {
                throw new ArgumentException(message: "Payment status must not be equal to null.");
            }
            if (value.Id != StatusId)
            {
                throw new ArgumentException(message: "The payment status ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
    public PaymentMethod? Method
    {
        get; private set
        {
            if (value is null)
            {
                throw new ArgumentException(message: "Payment method must not be equal to null.");
            }
            if (value.Id != MethodId)
            {
                throw new ArgumentException(message: "The payment method ID does not match the ID within the relationship.");
            }
            field = value;
        }
    }
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
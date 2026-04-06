using Domain.Abstract;

namespace Domain.Entity.Payment;

public sealed class PaymentStatus : StatusObjectAbstract<PaymentStatus>
{
    #region Constructor
    private PaymentStatus(string name) : base(name) { }
    #endregion
    #region Default objects
    public static readonly PaymentStatus Paid = new(nameof(Paid));
    public static readonly PaymentStatus NotPaid = new(nameof(NotPaid));
    public static readonly PaymentStatus Deleted = new(nameof(Deleted));
    public static readonly PaymentStatus Terminated = new(nameof(Terminated));
    #endregion
    #region Navigation properties
    public ICollection<Payment>? Payments { get; private set; }
    #endregion
}
using Domain.Abstract;

namespace Domain.Entity.Payment;

public sealed class PaymentStatus : StatusObjectAbstract<PaymentStatus>
{
    #region Constructor
    private PaymentStatus(int id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly PaymentStatus Paid = new(0, nameof(Paid));
    public static readonly PaymentStatus NotPaid = new(1, nameof(NotPaid));
    public static readonly PaymentStatus Deleted = new(2, nameof(Deleted));
    public static readonly PaymentStatus Terminated = new(3, nameof(Terminated));
    #endregion
}
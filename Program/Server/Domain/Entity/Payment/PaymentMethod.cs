using Domain.Abstract;

namespace Domain.Entity.Payment;

public sealed class PaymentMethod : StatusObjectAbstract<PaymentMethod>
{
    #region Constructor
    private PaymentMethod(string name): base(name) { }
    #endregion
    #region Default objects
    public static readonly PaymentMethod Card = new(nameof(Card));
    public static readonly PaymentMethod Cash = new(nameof(Cash));
    public static readonly PaymentMethod Sbp = new(nameof(Sbp));
    #endregion
    #region Navigation properties
    public ICollection<Payment>? Payments { get; private set; }
    #endregion
}
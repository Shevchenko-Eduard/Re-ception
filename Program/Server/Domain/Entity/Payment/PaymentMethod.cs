using Domain.Abstract;

namespace Domain.Entity.Payment;

public sealed class PaymentMethod : StatusObjectAbstract<PaymentMethod>
{
    #region Constructor
    private PaymentMethod(int id, string name): base(id, name) { }
    #endregion
    #region Default objects
    public static readonly PaymentMethod Card = new(0, nameof(Card));
    public static readonly PaymentMethod Cash = new(1, nameof(Cash));
    public static readonly PaymentMethod Sbp = new(2, nameof(Sbp));
    #endregion
}
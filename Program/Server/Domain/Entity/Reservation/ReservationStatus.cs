using Domain.Abstract;

namespace Domain.Entity.Reservation;

public sealed class ReservationStatus : StatusObjectAbstract<ReservationStatus>
{
    #region Constructors
    private ReservationStatus(ushort id, string name) : base(id, name) { }
    #endregion
    #region Default objects
    public static readonly ReservationStatus New = new(0, "New");
    public static readonly ReservationStatus Confirmed = new(1, "Confirmed");
    public static readonly ReservationStatus Guaranteed = new(2, "Guaranteed");
    public static readonly ReservationStatus Cancelled = new(3, "Cancelled");
    public static readonly ReservationStatus CheckedIn = new(4, "CheckedIn");
    public static readonly ReservationStatus Rejected = new(5, "Rejected");
    #endregion
}
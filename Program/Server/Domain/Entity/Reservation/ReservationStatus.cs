using Domain.Abstract;

namespace Domain.Entity.Reservation;

public sealed class ReservationStatus : StatusObjectAbstract<ReservationStatus>
{
    #region Constructors
    private ReservationStatus(string name) : base(name) { }
    #endregion
    #region Default objects
    public static readonly ReservationStatus New = new("New");
    public static readonly ReservationStatus Confirmed = new("Confirmed");
    public static readonly ReservationStatus Guaranteed = new("Guaranteed");
    public static readonly ReservationStatus Cancelled = new("Cancelled");
    public static readonly ReservationStatus CheckedIn = new("CheckedIn");
    public static readonly ReservationStatus Rejected = new("Rejected");
    #endregion
    #region Navigation properties
    public ICollection<Reservation>? Reservations { get; private set; }
    #endregion
}
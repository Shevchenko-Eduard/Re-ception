namespace Domain.Entity.Reservation;

public sealed class ReservationStatus
{
    public ushort Id { get; init; }
    public string Name { get; init; }
    private ReservationStatus(ushort id, string name)
    {
        Id = id;
        Name = name;
    }
    public static readonly ReservationStatus New = new(0, "New");
    public static readonly ReservationStatus Confirmed = new(1, "Confirmed");
    public static readonly ReservationStatus Guaranteed = new(2, "Guaranteed");
    public static readonly ReservationStatus Cancelled = new(3, "Cancelled");
    public static readonly ReservationStatus CheckedIn = new(4, "CheckedIn");
    public static readonly ReservationStatus Rejected = new(5, "Rejected");
    public static readonly List<ReservationStatus> All = [New, Confirmed, Guaranteed, Cancelled, CheckedIn, Rejected];
    public static ReservationStatus FromName(string name)
    {
        return All.FirstOrDefault(s => s.Name == name)
            ?? throw new ArgumentException($"Invalid reservation status name: {name}");
    }
    public static ReservationStatus FromId(ushort id)
    {
        return All.FirstOrDefault(s => s.Id == id)
            ?? throw new ArgumentException($"Invalid reservation status id: {id}");
    }
}
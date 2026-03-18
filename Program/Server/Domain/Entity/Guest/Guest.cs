using Domain.Interfaces;

namespace Domain.Entity.Guest;

public sealed class Guest
{
    #region Constants
    private const ushort _maxFirstName = 50;
    private const ushort _maxLastName = 50;
    #endregion
    #region Interfaces
    private readonly IClock _clock;
    #endregion
    #region Fields
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public DateTimeOffset CreateAt
    {
        get; init
        {
            if (value > _clock.Now)
            {
                throw new ArgumentException(message: "The creation date cannot be in the future.");
            }
            field = value;
        }
    }
    #endregion
    #region Navigation properties
    public IEnumerable<Reservation.Reservation>? Reservations { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private Guest() { }
#pragma warning restore CS9264, CS8618
    public Guest(
        Guid userId,
        IClock clock)
    {
        UserId = userId;
        _clock = clock;
        CreateAt = clock.Now;
    }
    #endregion
}
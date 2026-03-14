namespace Domain.Entity.Guest;

public sealed class Guest
{
    #region Constants
    private const ushort _maxFirstName = 50;
    private const ushort _maxLastName = 50;
    #endregion
    #region Fields
    public Guid Id { get; init; }
    public string FirstName
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxFirstName)
            {
                throw new ArgumentException(message: $"The line must not be longer than {_maxFirstName} characters.");
            }
            field = value;
        }
    }
    public string? LastName
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxLastName)
            {
                throw new ArgumentException(message: $"The line must not be longer than {_maxLastName} characters.");
            }
            field = value;
        }
    }
    public Phone? Phone { get; private set; }
    public Email Email { get; private set; }
    public DateOnly DateOfBirth
    {
        get; private set
        {
            if (value > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException(message: "The time of birth cannot be later than the current time.");
            }
            field = value;
        }
    }
    public DateTimeOffset CreateAt
    {
        get; init
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException(message: "The creation date cannot be in the future.");
            }
            field = value;
        }
    }
    public byte GenderId { get; private set; } = 0;
    public string PasswordHash
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            field = value;
        }
    }
    #endregion
    #region Navigation properties
    public GuestGender? Gender { get; private set; }
    public IEnumerable<Reservation.Reservation>? Reservations { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private Guest() { }
#pragma warning restore CS9264, CS8618
    public Guest(
        string firstName,
        Email email,
        DateOnly dateOfBirth,
        string passwordHash)
    {
        FirstName = firstName;
        DateOfBirth = dateOfBirth;
        Email = email;
        CreateAt = DateTimeOffset.Now;
        PasswordHash = passwordHash;
    }
    public Guest(
        string firstName,
        Email email,
        string passwordHash,
        DateOnly dateOfBirth,
        string? lastName = null,
        Phone? phone = null,
        int? genderId = null) : this(
            firstName: firstName,
            email: email,
            dateOfBirth: dateOfBirth,
            passwordHash: passwordHash)
    {
        if (lastName is not null) { LastName = lastName; }
        if (phone is not null) { Phone = phone; }
        if (email is not null) { Email = email; }
        if (genderId is not null) { GenderId = (byte)genderId; }
    }
    #endregion
}
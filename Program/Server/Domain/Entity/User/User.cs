using Domain.Interfaces;

namespace Domain.Entity.User;

public class User
{
    #region Constants
    private const ushort _maxUserName = 50;
    #endregion
    #region Interfaces
    private readonly IClock _clock;
    #endregion
    #region Fields
    public Guid Id { get; init; }
    public string UserName
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxUserName)
            {
                throw new ArgumentException(message: $"The line must not be longer than {_maxUserName} characters.");
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
            if (value > DateOnly.FromDateTime(_clock.Now.DateTime))
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
            if (value > _clock.Now)
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
    public UserGender? Gender { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private User() { }
#pragma warning restore CS9264, CS8618
    public User(
        string userName,
        Email email,
        DateOnly dateOfBirth,
        string passwordHash,
        IClock clock)
    {
        _clock = clock;
        UserName = userName;
        DateOfBirth = dateOfBirth;
        Email = email;
        CreateAt = clock.Now;
        PasswordHash = passwordHash;
    }
    public User(
        string userName,
        Email email,
        DateOnly dateOfBirth,
        string passwordHash,
        IClock clock,
        Phone phone) : this(
            userName: userName,
            email: email,
            dateOfBirth: dateOfBirth,
            passwordHash: passwordHash,
            clock: clock
        )
    {
        Phone = phone;
    }
    #endregion
}
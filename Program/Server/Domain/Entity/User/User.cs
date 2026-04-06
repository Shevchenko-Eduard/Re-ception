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
    public byte GenderId { get; private set; }
    #endregion
    #region Navigation properties
    public UserGender? UserGender { get; private set; }
    public ICollection<UserRole>? UserRoles { get; private set; }
    public ICollection<UserPermission>? UserPermissions { get; private set; }
    public ICollection<UserRole>? UserRolesAuthor { get; private set; }
    public ICollection<UserPermission>? UserPermissionsAuthor { get; private set; }
    public Employee.Employee? Employee { get; private set; }
    public Guest.Guest? Guest { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private User() { }
#pragma warning restore CS9264, CS8618
    public User(
        string userName,
        DateOnly dateOfBirth,
        IClock clock,
        byte genderId)
    {
        Id = Guid.NewGuid();
        _clock = clock;
        GenderId = genderId;
        UserName = userName;
        DateOfBirth = dateOfBirth;
        CreateAt = clock.Now;
    }
    #endregion
    #region Methods
    public void UpdateUserName(string userName)
    {
        UserName = userName;
    }
    public void UpdateDateOfBirth(DateOnly dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }
    public void UpdateGenderId(byte genderId)
    {
        GenderId = genderId;
    }
    #endregion
}
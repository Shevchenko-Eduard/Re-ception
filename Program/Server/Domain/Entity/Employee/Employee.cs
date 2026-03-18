using Domain.Entity.User;
using Domain.Entity.User.Role;
using Domain.Interfaces;

namespace Domain.Entity.Employee;

public sealed class Employee
{
    #region Constants
    private const ushort _maxFirstName = 50;
    private const ushort _maxLastName = 50;
    private const ushort _maxPatronymicName = 50;
    #endregion
    #region Interfaces
    private readonly IClock _clock;
    #endregion
    #region Fields
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public ushort HotelId { get; private set; }
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
    public string LastName
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
    public string? Patronymic
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxPatronymicName)
            {
                throw new ArgumentException(message: $"The line must not be longer than {_maxPatronymicName} characters.");
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
    public DateTimeOffset HireDate { get; init; }
    #endregion
    #region Navigation properties
    public Hotel.Hotel? Hotel { get; private set; }
    public IEnumerable<Role>? Roles { get; private set; }
    public UserGender? Gender { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private Employee() { }
#pragma warning restore CS9264, CS8618
    public Employee(
        int hotelId,
        Guid userId,
        string firstName,
        string lastName,
        DateTimeOffset hireDate,
        IClock clock)
    {
        UserId = userId;
        _clock = clock;
        HotelId = (ushort)hotelId;
        FirstName = firstName;
        LastName = lastName;
        HireDate = hireDate;
        CreateAt = _clock.Now;
    }
    public Employee(
        int hotelId,
        Guid userId,
        string firstName,
        string lastName,
        string patronymic,
        DateTimeOffset hireDate,
        IClock clock) : this(
            hotelId: hotelId,
            userId: userId,
            firstName: firstName,
            lastName: lastName,
            hireDate: hireDate,
            clock: clock
        )
    {
        Patronymic = patronymic;
    }
    #endregion
}
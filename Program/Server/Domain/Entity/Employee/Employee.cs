using Domain.Entity.Employee.Role;

namespace Domain.Entity.Employee;

public sealed class Employee
{
    #region Constants
    private const ushort _maxFirstName = 50;
    private const ushort _maxLastName = 50;
    private const ushort _maxPatronymicName = 50;
    #endregion
    #region Fields
    public Guid Id { get; init; }
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
    public Phone Phone { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            field = value;
        }
    }
    public DateOnly DateOfBirth
    {
        get;
        private set
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
    public DateTimeOffset HireDate { get; init; }
    public byte GenderId { get; private set; } = 0;
    #endregion
    #region Navigation properties
    public Hotel.Hotel? Hotel { get; private set; }
    public IEnumerable<EmployeeRole>? Roles { get; private set; }
    public EmployeeGender? Gender { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private Employee() { }
#pragma warning restore CS9264, CS8618
    public Employee(
        int hotelId,
        string firstName,
        string lastName,
        string patronymic,
        Phone phone,
        Email email,
        string passwordHash,
        DateOnly dateOfBirth,
        DateTimeOffset hireDate,
        int genderId)
    {
        HotelId = (ushort)hotelId;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        Phone = phone;
        Email = email;
        PasswordHash = passwordHash;
        DateOfBirth = dateOfBirth;
        HireDate = hireDate;
        CreateAt = DateTimeOffset.Now;
        GenderId = (byte)genderId;
    }
    #endregion
}
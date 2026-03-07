namespace Domain.Entity.Guest;

public sealed class Guest
{
    private const ushort _maxFirstName = 50;
    private const ushort _maxLastName = 50;
    private const ushort _maxPatronymicName = 50;
    private const ushort _maxNickname = 50;
    public ulong? Id { get; init; }
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
    public string? Nickname
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxNickname)
            {
                throw new ArgumentException(message: $"The line must not be longer than {_maxNickname} characters.");
            }
            field = value;
        }
    }
    public Phone? Phone { get; private set; }
    public Email? Email { get; private set; }
    public DateTimeOffset DateOfBirth
    {
        get; private set
        {
            if (value > DateTime.Now)
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
    public GenderEnum Gender { get; private set; } = GenderEnum.Indeterminate;
    public string PasswordHash
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            field = value;
        }
    }
#pragma warning disable CS9264
    private Guest() { }
#pragma warning restore CS9264
    public Guest(string firstName, DateTime dateOfBirth, string passwordHash)
    {
        FirstName = firstName;
        DateOfBirth = dateOfBirth;
        CreateAt = DateTimeOffset.Now;
        PasswordHash = passwordHash;
    }
    public Guest(
        string firstName,
        string passwordHash,
        DateTime dateOfBirth,
        string? lastName = null,
        string? patronymic = null,
        string? nickname = null,
        Phone? phone = null,
        Email? email = null,
        GenderEnum gender = GenderEnum.Indeterminate) : this(
            firstName: firstName,
            dateOfBirth: dateOfBirth,
            passwordHash: passwordHash)
    {
        if (lastName is not null)
        {
            LastName = lastName;
        }
        if (patronymic is not null)
        {
            Patronymic = patronymic;
        }
        if (nickname is not null)
        {
            Nickname = nickname;
        }
        if (phone is not null)
        {
            Phone = phone;
        }
        if (email is not null)
        {
            Email = email;
        }
        Gender = gender;
    }
}
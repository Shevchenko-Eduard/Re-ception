namespace Domain;

public sealed class Guest
{
    public ulong? GuestId { get; init; }
    public string FirstName
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 50)
            {
                throw new ArgumentException(message: "The line must not be longer than 50 characters.");
            }
            field = value;
        }
    }
    public string? LastName
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 50)
            {
                throw new ArgumentException(message: "The line must not be longer than 50 characters.");
            }
            field = value;
        }
    }
    public string? Patronymic
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 50)
            {
                throw new ArgumentException(message: "The line must not be longer than 50 characters.");
            }
            field = value;
        }
    }
    public string? Nickname
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > 50)
            {
                throw new ArgumentException(message: "The line must not be longer than 50 characters.");
            }
            field = value;
        }
    }
    public Phone? Phone { get; private set; }
    public Email? Email { get; private set; }
    public DateTime DateOfBirth
    {
        get => field.ToLocalTime();
        private set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException(message: "The time of birth cannot be later than the current time.");
            }
            field = value.ToUniversalTime();
        }
    }
    public DateTime CreateAt
    {
        get => field.ToLocalTime();
        init
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException(message: "The creation date cannot be in the future.");
            }
            field = value.ToUniversalTime();
        }
    }
    public GenderEnum Gender { get; private set; } = GenderEnum.Indeterminate;
#pragma warning disable CS9264
    private Guest() { }
#pragma warning restore CS9264
    public Guest(string firstName, DateTime dateOfBirth)
    {
        FirstName = firstName;
        DateOfBirth = dateOfBirth;
        CreateAt = DateTime.Now;
    }
    public Guest(
        string firstName,
        DateTime dateOfBirth,
        string? lastName = null,
        string? patronymic = null,
        string? nickname = null,
        string? phone = null,
        string? email = null,
        GenderEnum gender = GenderEnum.Indeterminate) : this(
            firstName: firstName,
            dateOfBirth: dateOfBirth)
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
            Phone = new(phone);
        }
        if (email is not null)
        {
            Email = new(email);
        }
        Gender = gender;
    }
}
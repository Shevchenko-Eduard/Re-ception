namespace Domain.Entity.Hotel;

public class Hotel
{
    #region Constants
    private const ushort _maxName = 50;
    private const ushort _maxCountry = 50;
    private const ushort _maxCity = 50;
    private const ushort _maxAddress = 100;
    #endregion
    #region Fields
    public byte Id { get; private set; }
    public string Name
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxName)
            {
                throw new ArgumentException(message: $"The name must not exceed {_maxName} characters.");
            }
            field = value;
        }
    }
    public string Country
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxCountry)
            {
                throw new ArgumentException(message: $"The country must not exceed {_maxCountry} characters.");
            }
            field = value;
        }
    }
    public string City
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxCity)
            {
                throw new ArgumentException(message: $"The city must not exceed {_maxCity} characters.");
            }
            field = value;
        }
    }
    public string Address
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxAddress)
            {
                throw new ArgumentException(message: $"The address must not exceed {_maxAddress} characters.");
            }
            field = value;
        }
    }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    #endregion
    #region Navigation properties
    public ICollection<Employee.Employee>? Employees { get; private set; }
    public ICollection<Room.Room>? Rooms { get; private set; }
    public ICollection<HotelTag>? HotelTags { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private Hotel() { }
#pragma warning restore CS9264, CS8618
    public Hotel(
        string name, string country,
        string city, string address,
        Email email, Phone phone)
    {
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Email = email;
        Phone = phone;
    }
    #endregion
}
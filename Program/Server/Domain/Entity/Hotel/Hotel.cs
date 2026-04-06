namespace Domain.Entity.Hotel;

public class Hotel
{
    #region Constants
    private const ushort _maxRegion = 50;
    private const ushort _maxCountry = 50;
    private const ushort _maxCity = 50;
    private const ushort _maxStreet = 50;
    private const ushort _maxAddress = 100;
    #endregion
    #region Fields
    public byte Id { get; private set; }
    public string Region
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxRegion)
            {
                throw new ArgumentException(message: $"The name must not exceed {_maxRegion} characters.");
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
    public string Street
    {
        get; private set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (value.Length > _maxStreet)
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
    public double Latitude
    {
        get; private set
        {
            if (value > 90 || value < -90)
            {
                throw new ArgumentException();
            }
            field = value;
        }
    }
    public double Longitude
    {
        get; private set
        {
            if (value > 180 || value < -180)
            {
                throw new ArgumentException();
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
        string region, string country,
        string city, string address,
        Email email, Phone phone,
        double latitude, double longitude,
        string street)
    {
        Street = street;
        Latitude = latitude;
        Longitude = longitude;
        Region = region;
        Country = country;
        City = city;
        Address = address;
        Email = email;
        Phone = phone;
    }
    #endregion
    #region Methods
    public void UpdateCountry(string country)
    {
        Country = country;
    }
    public void UpdateRegion(string region)
    {
        Region = region;
    }
    public void UpdateCity(string city)
    {
        City = city;
    }
    public void UpdateStreet(string street)
    {
        Street = street;
    }
    public void UpdateAddress(string address)
    {
        Address = address;
    }
    public void UpdateLatitude(double latitude)
    {
        Latitude = latitude;
    }
    public void UpdateLongitude(double longitude)
    {
        Longitude = longitude;
    }
    public void UpdateEmail(Email email)
    {
        Email = email;
    }
    public void UpdatePhone(Phone phone)
    {
        Phone = phone;
    }
    public void AddHotelTag(HotelTag hotelTag)
    {
        HotelTags ??= new HashSet<HotelTag>();
        HotelTags.Add(hotelTag);
    }
    public void RemoveHotelTag(HotelTag hotelTag)
    {
        HotelTags?.Remove(hotelTag);
    }
    #endregion
}
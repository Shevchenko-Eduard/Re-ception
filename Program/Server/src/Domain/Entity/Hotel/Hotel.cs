using Domain.Exception;

namespace Domain.Entity.Hotel;

public class Hotel
{
    #region Constants
    private const double _maxLatitude = 90;
    private const double _minLatitude = -90;
    private const double _maxLongitude = 180;
    private const double _minLongitude = -180;
    #endregion
    #region Fields
    public int Id
    {
        get; private set
        {
            if (value < 0) { throw new DomainExternalException(); }
            field = value;
        }
    }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    // Географическая широта
    public double Latitude
    {
        get; private set
        {
            if (value > _maxLatitude || value < _minLatitude)
            {
                throw new DomainExternalException();
            }
            field = value;
        }
    }
    // Географическая долгота
    public double Longitude
    {
        get; private set
        {
            if (value > _maxLongitude || value < _minLongitude)
            {
                throw new DomainExternalException();
            }
            field = value;
        }
    }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    #endregion
    #region Navigation properties
    public ICollection<HotelHotelTag>? HotelHotelTags { get; private set; }
    public ICollection<HotelImage>? HotelImages { get; private set; }
    public ICollection<Room.Room>? Rooms { get; private set; }
    #endregion
    #region Constructors
#pragma warning disable CS9264, CS8618
    private Hotel() { }
#pragma warning restore CS9264, CS8618
    public Hotel(
        string name,
        string? email, string? phone,
        double latitude, double longitude,
        string? description = null)
    {
        Name = name;
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
        Email = email;
        Phone = phone;
    }
    #endregion
    #region Methods
    public void UpdateLatitude(double latitude) => Latitude = latitude;
    public void UpdateLongitude(double longitude) => Longitude = longitude;
    public void UpdateEmail(string? email) => Email = email;
    public void UpdatePhone(string? phone) => Phone = phone;
    public void UpdateName(string name) => Name = name;
    public void UpdateDescription(string? description) => Description = description;
    #endregion
}
using Domain.Entity.Hotel;

namespace Application.Dto.Input;

public static class HotelDto
{
    public record Create(
        string Country,
        string Region,
        string City,
        string Street,
        string Address,
        double Latitude,
        double Longitude,
        string Email,
        string Phone
    )
    {
        public Hotel GetHotel() => new(
            region: Region,
            country: Country,
            city: City,
            address: Address,
            email: new(Email),
            phone: new(Phone),
            latitude: Latitude,
            longitude: Longitude,
            street: Street);
    }
    public record Update(
        byte Id,
        string? Country = null,
        string? Region = null,
        string? City = null,
        string? Street = null,
        string? Address = null,
        double? Latitude = null,
        double? Longitude = null,
        string? Email = null,
        string? Phone = null
    )
    {
        public Hotel GetHotel(Hotel oldHotel)
        {
            if (Country is not null)
            {
                oldHotel.UpdateCountry(Country);
            }
            if (Region is not null)
            {
                oldHotel.UpdateRegion(Region);
            }
            if (City is not null)
            {
                oldHotel.UpdateCity(City);
            }
            if (Street is not null)
            {
                oldHotel.UpdateStreet(Street);
            }
            if (Address is not null)
            {
                oldHotel.UpdateAddress(Address);
            }
            if (Latitude is not null)
            {
                oldHotel.UpdateLatitude((double)Latitude);
            }
            if (Longitude is not null)
            {
                oldHotel.UpdateLongitude((double)Longitude);
            }
            if (Email is not null)
            {
                oldHotel.UpdateEmail(new(Email));
            }
            if (Phone is not null)
            {
                oldHotel.UpdatePhone(new(Phone));
            }
            return oldHotel;
        }
    }
    public record Delete(
        byte Id
    );
    public record GetAll(
        string Country,
        string Region,
        string City,
        string Street,
        string Address,
        double Latitude,
        double Longitude,
        string Email,
        string Phone
    )
    {
        public static GetAll FromHotel(Hotel hotel) => new(
            Country: hotel.Country,
            Region: hotel.Region,
            City: hotel.City,
            Street: hotel.Street,
            Address: hotel.Address,
            Latitude: hotel.Latitude,
            Longitude: hotel.Longitude,
            Email: hotel.Email.Value,
            Phone: hotel.Phone.Value
        );
        public static IEnumerable<GetAll> FromListHotels(IEnumerable<Hotel> hotels)
        {
            return hotels.Select(FromHotel);
        }
    }
}
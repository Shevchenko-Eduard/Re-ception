using Domain.Entity.Hotel;

namespace Application.DTOs;

public static class HotelDTOs
{
    public record Create(
        string Name,
        string Email,
        string Phone,
        double Latitude,
        double Longitude,
        string? Description = null
    )
    {
        public Hotel GetHotel() => new(
            name: Name,
            email: new(Email),
            phone: new(Phone),
            latitude: Latitude,
            longitude: Longitude,
            description: Description);
    }
    public record Update(
        int Id,
        double? Latitude = null,
        double? Longitude = null,
        string? Email = null,
        string? Phone = null,
        string? Name = null,
        string? Description = null
    )
    {
        public Hotel GetHotel(Hotel hotel)
        {
            hotel.UpdateLatitude(Latitude ?? hotel.Latitude);
            hotel.UpdateLongitude(Longitude ?? hotel.Longitude);
            hotel.UpdateEmail(Email is not null ? new(Email) : hotel.Email);
            hotel.UpdatePhone(Phone is not null ? new(Phone) : hotel.Phone);
            hotel.UpdateName(Name ?? hotel.Name);
            hotel.UpdateDescription(Description ?? hotel.Description);
            return hotel;
        }
    }
    public record Delete(
        int Id
    );
}
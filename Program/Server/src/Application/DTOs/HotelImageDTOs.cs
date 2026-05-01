using System.Linq.Expressions;
using Domain.Entity.Hotel;

namespace Application.DTOs;

public static class HotelImageDTOs
{
    public record Create(
        int HotelId,
        Stream Stream
    )
    {
        public HotelImage GetImage() => new(hotelId: HotelId);
    }
    public record Update(
        int Id,
        Stream Stream
    );
    public record Delete(
        int Id
    );
}
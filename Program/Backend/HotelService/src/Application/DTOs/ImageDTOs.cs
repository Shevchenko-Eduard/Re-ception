using System.Linq.Expressions;
using Domain.Entity.Hotel;

namespace Application.DTOs;

public static class ImageDTOs
{
    public record Create(
        int HotelId,
        byte[] Bytes
    )
    {
        public Image GetImage() => new(hotelId: HotelId, bytes: Bytes);
    }
    public record Update(
        int Id,
        byte[]? Bytes = null
    )
    {
        public Image GetImage(Image image)
        {
            image.UpdateBytes(Bytes ?? image.Bytes);
            return image;
        }
    }
    public record Delete(
        int Id
    );
}
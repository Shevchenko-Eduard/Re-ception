using Domain.Entity.Hotel;

namespace Application.DTOs;

public static class HotelImageDTOs
{
    public static class Inner
    {
        public record Create(
            int HotelId,
            string Extension,
            string ContentType,
            Stream Stream
        )
        {
            public HotelImage GetHotelImage() => new(
                hotelId: HotelId,
                extension: Extension,
                contentType: ContentType);
        }
        public record Update(
            int Id,
            string? Extension,
            string? ContentType,
            Stream Stream
        )
        {
            public HotelImage GetHotelImage(HotelImage hotelImage)
            {
                hotelImage.UpdateExtension(Extension ?? hotelImage.Extension);
                hotelImage.UpdateContentType(ContentType ?? hotelImage.ContentType);
                return hotelImage;
            }
        }
    }
    public static class Request
    {
        public record Create(
            int HotelId
        );
        public record Update(
            int Id
        );
        public record Delete(
            int Id
        );
        public record Read(
            int Id
        );
    }
    public static class Response
    {
        public record Read(
            Stream Stream,
            string ContentType,
            string FileName
        );
    }
}
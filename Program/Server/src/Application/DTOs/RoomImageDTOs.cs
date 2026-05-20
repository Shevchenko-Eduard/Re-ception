using Domain.Entity.Room;

namespace Application.DTOs;

public static class RoomImageDTOs
{
    public static class Inner
    {
        public record Create(
            int RoomId,
            string Extension,
            string ContentType,
            Stream Stream
        )
        {
            public RoomImage GetRoomImage() => new(
                roomId: RoomId,
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
            public RoomImage GetRoomImage(RoomImage roomImage)
            {
                roomImage.UpdateExtension(Extension ?? roomImage.Extension);
                roomImage.UpdateContentType(ContentType ?? roomImage.ContentType);
                return roomImage;
            }
        }
    }
    public static class Request
    {
        public record Create(
            int RoomId
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
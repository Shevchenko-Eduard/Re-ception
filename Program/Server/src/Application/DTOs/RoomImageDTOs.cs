using Domain.Entity.Room;

namespace Application.DTOs;

public static class RoomImageDTOs
{
    public record Create(
        int RoomId,
        Stream Stream
    )
    {
        public RoomImage GetImage() => new(roomId: RoomId);
    }
    public record Update(
        int Id,
        Stream Stream
    );
    public record Delete(
        int Id
    );
}
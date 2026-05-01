using Domain.Entity.Room;

namespace Application.DTOs;

public static class RoomRoomTagDTOs
{
    public record Create(
        int RoomId,
        int RoomTagId
    )
    {
        public RoomRoomTag GetRoomRoomTag() => new(roomId: RoomId, roomTagId: RoomTagId);
    }

    public record Delete(
        int Id
    );
}

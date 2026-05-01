using Domain.Entity.Room;

namespace Application.DTOs;

public static class RoomTagDTOs
{
    public record Create(
        string Name,
        string Description
    )
    {
        public RoomTag GetRoomTag() => new(
            name: Name,
            description: Description
        );
    }

    public record Update(
        int Id,
        string? Name = null,
        string? Description = null
    )
    {
        public RoomTag GetUpdateRoomTag(RoomTag roomTag)
        {
            roomTag.UpdateName(Name ?? roomTag.Name);
            roomTag.UpdateDescription(Description ?? roomTag.Description);
            return roomTag;
        }
    }

    public record Delete(
        int Id
    );
}

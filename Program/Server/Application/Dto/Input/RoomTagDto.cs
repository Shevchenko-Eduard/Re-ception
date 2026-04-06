using Domain.Entity.Room;

namespace Application.Dto.Input;

public static class RoomTagDto
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
        ushort Id,
        string? Name = null,
        string? Description = null
    )
    {
        public RoomTag GetUpdateRoomTag(RoomTag roomTag)
        {
            if (Name is not null)
            {
                roomTag.UpdateName(Name);
            }
            if (Description is not null)
            {
                roomTag.UpdateDescription(Description);
            }
            
            return roomTag;
        }
    }

    public record Delete(
        ushort Id
    );
}

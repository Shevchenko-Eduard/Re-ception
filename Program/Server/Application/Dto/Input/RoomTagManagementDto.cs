namespace Application.Dto.Input;

public static class RoomTagManagementDto
{
    public record AddTag(
        ushort RoomId,
        ushort RoomTagId
    );

    public record RemoveTag(
        ushort RoomId,
        ushort RoomTagId
    );
}

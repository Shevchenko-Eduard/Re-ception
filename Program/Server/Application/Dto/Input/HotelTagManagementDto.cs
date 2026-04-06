namespace Application.Dto.Input;

public static class HotelTagManagementDto
{
    public record AddTag(
        byte HotelId,
        ushort HotelTagId
    );

    public record RemoveTag(
        byte HotelId,
        ushort HotelTagId
    );
}

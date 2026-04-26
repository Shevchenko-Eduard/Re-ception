using Domain.Entity.Hotel;

namespace Application.DTOs;

public static class HotelTagDTOs
{
    public record Create(
        int HotelId,
        int TagId
    )
    {
        public HotelTag GetHotelTag() => new(hotelId: HotelId, tagId: TagId);
    }

    public record Delete(
        int Id
    );
}

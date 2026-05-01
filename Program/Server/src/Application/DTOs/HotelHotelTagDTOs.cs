using Domain.Entity.Hotel;

namespace Application.DTOs;

public static class HotelHotelTagDTOs
{
    public record Create(
        int HotelId,
        int TagId
    )
    {
        public HotelHotelTag GetHotelTag() => new(hotelId: HotelId, tagId: TagId);
    }

    public record Delete(
        int Id
    );
}

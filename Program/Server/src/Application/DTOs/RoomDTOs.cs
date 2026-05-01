using Domain.Entity.Room;

namespace Application.DTOs;

public static class RoomDTOs
{
    public record Create(
        int HotelId,
        int RoomTypeId,
        int RoomNumber,
        int Floor,
        byte RoomStatusId,
        decimal? PricePerDay
    )
    {
        public Room GetRoom() => new(
            hotelId: HotelId,
            roomTypeId: RoomTypeId,
            roomNumber: RoomNumber,
            floor: Floor,
            roomStatusId: RoomStatusId,
            pricePerDay: PricePerDay
        );
    }
    public record Update(
        int Id,
        int? RoomTypeId = null,
        int? RoomNumber = null,
        int? Floor = null,
        byte? RoomStatusId = null,
        decimal? PricePerDay = null
    )
    {
        public Room GetUpdateRoom(Room room)
        {
            room.UpdateRoomTypeId(RoomTypeId ?? room.RoomTypeId);
            room.UpdateRoomNumber(RoomNumber ?? room.RoomNumber);
            room.UpdateFloor(Floor ?? room.Floor);
            room.UpdateRoomStatusId(RoomStatusId ?? room.RoomStatusId);
            room.UpdatePricePerDay(PricePerDay ?? room.PricePerDay);
            return room;
        }
    }
    public record Delete(
        int Id
    );
}
using Domain.Entity.Room;

namespace Application.Dto.Input;

public static class RoomDto
{
    public record Create(
        byte HotelId,
        ushort RoomTypeId,
        ushort RoomNumber,
        byte Floor,
        ushort RoomStatusId,
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
        ushort Id,
        ushort? RoomTypeId = null,
        ushort? RoomNumber = null,
        byte? Floor = null,
        ushort? RoomStatusId = null,
        decimal? PricePerDay = null
    )
    {
        public Room GetUpdateRoom(Room room)
        {
            if (RoomTypeId is not null)
            {
                room.UpdateRoomTypeId((ushort)RoomTypeId);
            }
            if (RoomNumber is not null)
            {
                room.UpdateRoomNumber((ushort)RoomNumber);
            }
            if (Floor is not null)
            {
                room.UpdateFloor((byte)Floor);
            }
            if (RoomStatusId is not null)
            {
                room.UpdateRoomStatusId((byte)RoomStatusId);
            }
            if (PricePerDay is not null)
            {
                room.UpdatePricePerDay((ushort)PricePerDay);
            }
            return room;
        }
    }
    public record Delete(
        ushort id
    );
}
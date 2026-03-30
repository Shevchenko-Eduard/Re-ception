using Application.Interfaces;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.Dto.Input;

public static class ReservationDto
{
    public record Create(
        Guid GuestId,
        uint RoomId,
        DateTimeOffset CheckIn,
        DateTimeOffset CheckOut)
    {
        public async Task<Reservation> GetReservation(
            IRoomRepository roomRepository,
            IRoomTypeRepository roomTypeRepository)
        {
            Reservation reservation = new(
                guestId: GuestId,
                roomId: RoomId,
                checkIn: CheckIn,
                checkOut: CheckOut);
            await reservation.UpdateTotalPrice(roomRepository, roomTypeRepository);
            return reservation;
        }
    }
    public record Delete(
        ulong Id){}
    public record Update(
        ulong Id,
        DateTimeOffset? CheckIn = null,
        DateTimeOffset? CheckOut = null,
        decimal? Discount = null,
        byte? ReservationStatusId = null
        ){}
}
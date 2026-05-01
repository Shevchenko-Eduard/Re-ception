using Domain.Entity.Reservation;
using Domain.Interfaces;

namespace Application.DTOs;

public static class ReservationDTOs
{
    public record Create(
        Guid GuestId,
        int RoomId,
        DateTimeOffset CheckIn,
        DateTimeOffset CheckOut)
    {
        public async Task<Reservation> GetReservation(
            ICalculatorReservationPrice calculatorReservationPrice)
        {
            Reservation reservation = new(
                calculatorPrice: calculatorReservationPrice,
                guestId: GuestId,
                roomId: RoomId,
                checkIn: CheckIn,
                checkOut: CheckOut);
            await reservation.UpdateTotalPrice();
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
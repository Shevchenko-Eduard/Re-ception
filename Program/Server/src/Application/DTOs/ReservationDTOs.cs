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
        int Id){}
    public record Update(
        int Id,
        DateTimeOffset? CheckIn = null,
        DateTimeOffset? CheckOut = null,
        decimal? Discount = null,
        byte? ReservationStatusId = null
        )
    {
        public async Task<Reservation> GetReservation(Reservation reservation)
        {
            bool isUpdateTotalPrice = false;
        if (CheckIn is not null)
        {
            reservation.UpdateCheckIn((DateTimeOffset)CheckIn);
            isUpdateTotalPrice = true;
        }
        if (CheckOut is not null)
        {
            reservation.UpdateCheckOut((DateTimeOffset)CheckOut);
            isUpdateTotalPrice = true;
        }
        if (Discount is not null)
        {
            reservation.UpdateDiscount((decimal)Discount);
            isUpdateTotalPrice = true;
        }
        if (Discount is not null)
        {
            reservation.UpdateDiscount((decimal)Discount);
        }
        if (isUpdateTotalPrice)
        {
            await reservation.UpdateTotalPrice();
        }
        return reservation;
        }
    }
}
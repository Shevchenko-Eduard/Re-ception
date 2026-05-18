using Domain.Entity.Reservation;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Domain.Service;

public class CalculatorReservationPrice(
    IRoomRepository roomRepository) : ICalculatorReservationPrice
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    public async Task<decimal> Calculator(Reservation reservation)
    {
        decimal pricePerDay = await _roomRepository.GetPricePerDay(reservation.RoomId);
        TimeSpan reservationTimeSpan = reservation.CheckOut - reservation.CheckIn;
        decimal reservationDays = (decimal)reservationTimeSpan.TotalDays;
        decimal totalPrice = reservationDays * pricePerDay;
        if (reservation.Discount is not null)
        {
            totalPrice *= (decimal)(1 - reservation.Discount);
        }
        return totalPrice;
    }
}

using System.Runtime.Serialization;
using Domain.Entity.Reservation;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Domain.Service;

public class CalculatorReservationPrice(
    IRoomRepository roomRepository,
    IRoomTypeRepository roomTypeRepository) : ICalculatorReservationPrice
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    public async Task<decimal> Calculator(Reservation reservation)
    {
        if (reservation.Room is null)
        {
            reservation.SetRoom(await _roomRepository.GetByIdAsync(reservation.RoomId)
                ?? throw new SystemException());
        }
        if (reservation.Room!.PricePerDay is null && reservation.Room.RoomType is null)
        {
            reservation.Room.SetRoomType(await _roomTypeRepository.GetByIdAsync(reservation.Room.RoomTypeId)
                ?? throw new SystemException());
        }
        var pricePerDay = reservation.Room.PricePerDay is null
            ? reservation.Room.RoomType!.BasePricePerDay
            : reservation.Room.PricePerDay
            ?? throw new SystemException();
        var reservationTimeSpan = reservation.CheckOut - reservation.CheckIn;
        var reservationDays = (decimal)reservationTimeSpan.TotalDays;
        var totalPrice = reservationDays * pricePerDay;
        if (reservation.Discount is not null)
        {
            totalPrice *= (decimal)(1 - reservation.Discount);
        }
        return totalPrice;
    }
}

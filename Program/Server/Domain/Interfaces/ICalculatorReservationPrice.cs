using Domain.Entity.Reservation;

namespace Domain.Interfaces;

public interface ICalculatorReservationPrice
{
    Task<decimal> Calculator(Reservation reservation);
}

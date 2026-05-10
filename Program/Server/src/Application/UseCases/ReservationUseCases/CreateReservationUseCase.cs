using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Reservation;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.ReservationUseCases;

public class CreateReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    ICalculatorReservationPrice calculatorReservationPrice) : IAction<ReservationDTOs.Create, Reservation>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ICalculatorReservationPrice _calculatorReservationPrice = calculatorReservationPrice;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Reservation> Execute(ReservationDTOs.Create input)
    {
        var reservation = await input.GetReservation(_calculatorReservationPrice);
        await _reservationRepository.AddAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
        return reservation;
    }
}
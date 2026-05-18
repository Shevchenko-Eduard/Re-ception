using Application.Exception;
using Application.Interfaces;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.ReservationUseCases;

public class UpdateReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork) : IAction<DTOs.ReservationDTOs.Update, Reservation>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<Reservation> Execute(DTOs.ReservationDTOs.Update input)
    {
        Reservation reservation = await _reservationRepository.GetByIdAsync(input.Id)
            ?? throw new ApplicationExternalException("Reservation with the specified ID not found");
        Reservation updateReservation = await input.GetReservation(reservation);
        await _reservationRepository.UpdateAsync(updateReservation);
        await _unitOfWork.SaveChangesAsync();
        return updateReservation;
    }
}
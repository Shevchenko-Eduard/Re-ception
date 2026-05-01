using Application.Interfaces;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.ReservationUseCases;

public class DeleteReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork) : IAction<DTOs.ReservationDTOs.Delete>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(DTOs.ReservationDTOs.Delete input)
    {
        Reservation reservation = await _reservationRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("Reservation with the specified ID not found");
        await _reservationRepository.DeleteAsync(reservation.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
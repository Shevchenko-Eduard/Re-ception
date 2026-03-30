using Application.Interfaces;
using Domain.Entity.Guest;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.ReservationUseCases;

public class DeleteReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser)
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurrentUser _currentUser = currentUser;
    public async Task Execute(Dto.Input.ReservationDto.Delete delete)
    {
        Guest currentGuest = await _currentUser.GetGuestAsync()
            ?? throw new ArgumentException();
        Reservation reservation = await _reservationRepository.GetByIdAsync(delete.Id)
            ?? throw new ArgumentException();
        if (reservation.GuestId != currentGuest.Id)
        {
            throw new ArgumentException();
        }
        await _reservationRepository.DeleteAsync(reservation.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
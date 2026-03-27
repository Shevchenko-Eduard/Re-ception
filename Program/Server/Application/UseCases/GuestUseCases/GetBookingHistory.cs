using Application.Interfaces;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.GuestUseCases;

public class GetBookingHistory
{
    private readonly ICurrentUser _currentUser;
    private readonly IReservationRepository _reservationRepository;
    public GetBookingHistory(
        ICurrentUser currentUser,
        IReservationRepository reservationRepository)
    {
        _currentUser = currentUser;
        _reservationRepository = reservationRepository;
    }
    public async Task<IEnumerable<Reservation>> Execute()
    {
        return await _reservationRepository.FindAsync(r => r.GuestId == _currentUser.CurrentUserId);
    }
}
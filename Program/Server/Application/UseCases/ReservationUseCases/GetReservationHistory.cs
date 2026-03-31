using Application.Dto.Output;
using Application.Interfaces;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.ReservationUseCases;

public class GetReservationHistory(
    IReservationRepository reservationRepository,
    ICurrentUser currentUser) : IUseCase<object, IEnumerable<ReservationDto.Response>>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<IEnumerable<ReservationDto.Response>> Execute(object input)
    {
        var currentGuest = await _currentUser.GetGuestAsync()
            ?? throw new SystemException();

        return (await _reservationRepository
            .FindAsync(r => r.GuestId == currentGuest.Id))
            .Select(ReservationDto.Response.FromReservation);
    }
}
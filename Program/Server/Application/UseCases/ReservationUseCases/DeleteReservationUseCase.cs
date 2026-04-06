using Application.Interfaces;
using Domain.Entity.Guest;
using Domain.Entity.Reservation;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.ReservationUseCases;

public class DeleteReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IAuthorization authorization) : IUseCase<Dto.Input.ReservationDto.Delete>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Delete, PermissionEntity.Reservation, PermissionFlag.Self);

    public async Task Execute(Dto.Input.ReservationDto.Delete input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException("User does not have permission to delete reservations");
        }
        Guest currentGuest = await _currentUserService.GetCurrentGuestAsync()
            ?? throw new ArgumentException("Current user is not a guest");
        Reservation reservation = await _reservationRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("Reservation with the specified ID not found");
        if (reservation.GuestId != currentGuest.Id)
        {
            throw new ArgumentException("You can only delete your own reservations");
        }
        await _reservationRepository.DeleteAsync(reservation.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
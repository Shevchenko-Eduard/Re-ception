using System.Data.Common;
using Application.Dto.Output;
using Application.Interfaces;
using Domain.Entity.User.Permission;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.ReservationUseCases;

public class GetReservationHistory(
    IReservationRepository reservationRepository,
    ICurrentUserService currentUserService,
    IAuthorization authorization) : IUseCase<object, IEnumerable<ReservationDto.Response>>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IAuthorization _authorization = authorization;

    public Permission RequiredPermission => new(PermissionAction.Read, PermissionEntity.Reservation, PermissionFlag.Self);

    public async Task<IEnumerable<ReservationDto.Response>> Execute(object input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }

        var currentGuest = await _currentUserService.GetCurrentGuestAsync()
            ?? throw new SystemException();

        return (await _reservationRepository
            .FindAsync(r => r.GuestId == currentGuest.Id))
            .Select(ReservationDto.Response.FromReservation);
    }
}
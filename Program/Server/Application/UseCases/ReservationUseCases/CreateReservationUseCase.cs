using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.User;
using Domain.Entity.User.Permission;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.ReservationRepository;

namespace Application.UseCases.ReservationUseCases;

public class CreateReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    IGuestRepository guestRepository,
    ICalculatorReservationPrice calculatorReservationPrice,
    IAuthorization authorization) : IUseCase<ReservationDto.Create>
{
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ICalculatorReservationPrice _calculatorReservationPrice = calculatorReservationPrice;
    private readonly IAuthorization _authorization = authorization;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Permission RequiredPermission => new(PermissionAction.Create, PermissionEntity.Reservation, PermissionFlag.Self);

    public async Task Execute(ReservationDto.Create input)
    {
        if (!await _authorization.Verify(RequiredPermission))
        {
            throw new ArgumentException();
        }
        _ = await _guestRepository.GetByIdAsync(input.GuestId)
            ?? throw new ArgumentException();
        var reservation = await input.GetReservation(_calculatorReservationPrice);
        await _reservationRepository.AddAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
    }
}
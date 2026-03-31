using Application.Dto.Input;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.ReservationRepository;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.ReservationUseCases;

public class CreateReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    IGuestRepository guestRepository,
    ICalculatorReservationPrice calculatorReservationPrice) : IUseCase<ReservationDto.Create>
{
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly ICalculatorReservationPrice _calculatorReservationPrice = calculatorReservationPrice;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(ReservationDto.Create input)
    {
        _ = await _guestRepository.GetByIdAsync(input.GuestId)
            ?? throw new ArgumentException();
        var reservation = await input.GetReservation(_calculatorReservationPrice);
        await _reservationRepository.AddAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
    }
}
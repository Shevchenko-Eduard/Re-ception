using Application.Interfaces;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.ReservationUseCases;

public class UpdateReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork) : IUseCase<Dto.Input.ReservationDto.Update>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(Dto.Input.ReservationDto.Update input)
    {
        Reservation reservation = await _reservationRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException();
        bool isUpdateTotalPrice = false;
        if (input.CheckIn is not null)
        {
            reservation.UpdateCheckIn((DateTimeOffset)input.CheckIn);
            isUpdateTotalPrice = true;
        }
        if (input.CheckOut is not null)
        {
            reservation.UpdateCheckOut((DateTimeOffset)input.CheckOut);
            isUpdateTotalPrice = true;
        }
        if (input.Discount is not null)
        {
            reservation.UpdateDiscount((decimal)input.Discount);
            isUpdateTotalPrice = true;
        }
        if (input.Discount is not null)
        {
            reservation.UpdateDiscount((decimal)input.Discount);
        }
        if (isUpdateTotalPrice)
        {
            await reservation.UpdateTotalPrice();
        }
        await _reservationRepository.UpdateAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
    }
}
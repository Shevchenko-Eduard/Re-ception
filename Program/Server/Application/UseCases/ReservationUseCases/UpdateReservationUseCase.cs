using Application.Interfaces;
using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.ReservationRepository;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.ReservationUseCases;

public class UpdateReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    IRoomTypeRepository roomTypeRepository,
    IRoomRepository roomRepository)
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(Dto.Input.ReservationDto.Update update)
    {
        Reservation reservation = await _reservationRepository.GetByIdAsync(update.Id)
            ?? throw new ArgumentException();
        bool isUpdateTotalPrice = false;
        if (update.CheckIn is not null)
        {
            reservation.UpdateCheckIn((DateTimeOffset)update.CheckIn);
            isUpdateTotalPrice = true;
        }
        if (update.CheckOut is not null)
        {
            reservation.UpdateCheckOut((DateTimeOffset)update.CheckOut);
            isUpdateTotalPrice = true;
        }
        if (update.Discount is not null)
        {
            reservation.UpdateDiscount((decimal)update.Discount);
            isUpdateTotalPrice = true;
        }
        if (update.Discount is not null)
        {
            reservation.UpdateDiscount((decimal)update.Discount);
        }
        if (isUpdateTotalPrice)
        {
            await reservation.UpdateTotalPrice(
                _roomRepository, _roomTypeRepository);
        }
        await _reservationRepository.UpdateAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
    }
}
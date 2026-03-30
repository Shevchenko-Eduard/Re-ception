using Application.Dto.Input;
using Application.Interfaces;
using Domain.Entity.Guest;
using Domain.Interfaces.Repositories.GuestRepository;
using Domain.Interfaces.Repositories.ReservationRepository;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.ReservationUseCases;

public class CreateReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    IGuestRepository guestRepository,
    IRoomRepository roomRepository,
    IRoomTypeRepository roomTypeRepository)
{
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(ReservationDto.Create create)
    {
        _ = await _guestRepository.GetByIdAsync(create.GuestId)
            ?? throw new ArgumentException();
        var reservation = await create.GetReservation(
            _roomRepository, _roomTypeRepository);
        await _reservationRepository.AddAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
    }
}
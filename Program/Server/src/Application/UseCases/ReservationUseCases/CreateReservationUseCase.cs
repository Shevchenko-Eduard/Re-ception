using System.Runtime.CompilerServices;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Reservation;
using Domain.Entity.Room;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.ReservationRepository;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.ReservationUseCases;

public class CreateReservationUseCase(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    ICalculatorReservationPrice calculatorReservationPrice,
    ICurrentUser currentUser,
    IRoomRepository roomRepository) : IAction<ReservationDTOs.Create, Reservation>
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly ICalculatorReservationPrice _calculatorReservationPrice = calculatorReservationPrice;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Reservation> Execute(ReservationDTOs.Create input)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var room = await _roomRepository.GetByIdAsync(input.RoomId)
                ?? throw new System.Exception("Room with this id is not exist.");
            await _roomRepository.LoadRoomStatusAsync(room);
            if (room.RoomStatus is null) { throw new System.Exception("Room status is null."); }
            if (room.RoomStatus != RoomStatus.Vacant) { throw new System.Exception("Room is not vacant."); }

            var reservation = await input.GetReservation(_calculatorReservationPrice, _currentUser);
            await _reservationRepository.AddAsync(reservation);
            room.UpdateRoomStatusId(RoomStatus.Reserved.Id);
            await _roomRepository.UpdateAsync(room);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return reservation;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
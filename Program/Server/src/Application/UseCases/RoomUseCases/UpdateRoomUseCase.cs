using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomUseCases;

public class UpdateRoomUseCase(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork) : IAction<RoomDTOs.Update, Room>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Room> Execute(RoomDTOs.Update input)
    {
        Room room = await _roomRepository.GetByIdAsync(input.Id)
            ?? throw new ArgumentException("Room with the specified ID not found");
        Room newRoom = input.GetUpdateRoom(room);
        await _roomRepository.UpdateAsync(newRoom);
        await _unitOfWork.SaveChangesAsync();
        return newRoom;
    }
}
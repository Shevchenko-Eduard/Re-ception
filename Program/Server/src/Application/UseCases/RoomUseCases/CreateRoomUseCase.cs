using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomUseCases;

public class CreateRoomUseCase(

    IUnitOfWork unitOfWork,
    IRoomRepository roomRepository) : IAction<RoomDTOs.Create, Room>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Room> Execute(RoomDTOs.Create input)
    {
        Room room = input.GetRoom();
        await _roomRepository.AddAsync(room);
        await _unitOfWork.SaveChangesAsync();
        return room;
    }
}
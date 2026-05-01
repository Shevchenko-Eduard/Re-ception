using Application.DTOs;
using Application.Interfaces;
using Domain.Entity.Room;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class AddRoomTagUseCase(
    IRoomRoomTagRepository roomRoomTagRepository,
    IUnitOfWork unitOfWork) : IAction<RoomRoomTagDTOs.Create>
{
    private readonly IRoomRoomTagRepository _roomRoomTagRepository = roomRoomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RoomRoomTagDTOs.Create input)
    {
        RoomRoomTag roomRoomTag = input.GetRoomRoomTag();
        await _roomRoomTagRepository.AddAsync(roomRoomTag);
        await _unitOfWork.SaveChangesAsync();
    }
}

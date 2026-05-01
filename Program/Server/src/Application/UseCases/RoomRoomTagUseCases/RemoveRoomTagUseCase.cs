using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class RemoveRoomTagUseCase(
    IRoomRoomTagRepository roomRoomTagRepository,
    IUnitOfWork unitOfWork) : IAction<RoomRoomTagDTOs.Delete>
{
    private readonly IRoomRoomTagRepository _roomRoomTagRepository = roomRoomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RoomRoomTagDTOs.Delete input)
    {
        await _roomRoomTagRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}

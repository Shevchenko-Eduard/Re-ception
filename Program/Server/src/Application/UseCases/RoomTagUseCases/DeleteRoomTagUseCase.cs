using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomTagUseCases;

public class DeleteRoomTagUseCase(
    IRoomTagRepository roomTagRepository,
    IUnitOfWork unitOfWork) : IAction<RoomTagDTOs.Delete>
{
    private readonly IRoomTagRepository _roomTagRepository = roomTagRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RoomTagDTOs.Delete input)
    {

        await _roomTagRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}

using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomUseCases;

public class DeleteRoomUseCase(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork) : IAction<RoomDTOs.Delete>
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RoomDTOs.Delete input)
    {
        await _roomRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}
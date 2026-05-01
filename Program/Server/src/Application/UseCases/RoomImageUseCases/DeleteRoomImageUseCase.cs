using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces.Repositories.RoomRepository;

namespace Application.UseCases.RoomImageUseCases;

public class DeleteRoomImageUseCase(
    IRoomImageRepository roomImageRepository,
    IUnitOfWork unitOfWork) : IAction<RoomImageDTOs.Delete>
{
    private readonly IRoomImageRepository _roomImageRepository = roomImageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(RoomImageDTOs.Delete input)
    {
        await _roomImageRepository.DeleteAsync(input.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}